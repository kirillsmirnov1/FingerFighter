using System;
using System.Collections.Generic;
using System.Linq;
using FingerFighter.Control.Common.Combat.Status;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace FingerFighter.Control.Common.Input.Handles
{
    [DefaultExecutionOrder(-1)]
    public class HandlesInputManager : MonoBehaviour
    {
        public static event Action OnInputLost;
        public static event Action OnInputRegained;
        
        [SerializeField] private Handle[] handles;

        private readonly Dictionary<Finger, Handle> _pairings = new Dictionary<Finger, Handle>();
        private List<Handle> _freeHandles;

        private static readonly object Lock = new object();
        private Camera _camera;
    
        private void OnValidate()
        {
            handles = GetComponentsInChildren<Handle>();
        }

        private void Awake()
        {
            _freeHandles = new List<Handle>(handles);
            EnhancedTouchSupport.Enable();
        }

        private void OnEnable()
        {
            _camera = Camera.main;
            Touch.onFingerDown += OnFingerDown;
            Touch.onFingerUp += OnFingerUp;
            PlayerStatus.OnDeath += OnPlayerDead;
        }

        private void OnDisable()
        {
            Touch.onFingerDown -= OnFingerDown;
            Touch.onFingerUp -= OnFingerUp;
            PlayerStatus.OnDeath -= OnPlayerDead;
        }

        private void OnFingerDown(Finger finger)
        {
            lock (Lock)
            {
                if (_freeHandles.Count <= 0) return;
                if (IsPointerOverUiComponent<Image>(finger.screenPosition)) return;
                var handle = PickHandleForFinger(finger);  
                handle.finger = finger;
                _pairings.Add(finger, handle);
                if(_pairings.Count == 1) OnInputRegained?.Invoke();
            }
        }

        private Handle PickHandleForFinger(Finger finger)
        {
            var nearestHandleIndex = 0;
            if (_freeHandles.Count > 1)
            {
                var fingerPos = _camera.ScreenToWorldPoint(finger.screenPosition);
                var minDistance = float.MaxValue;
                for (int i = 0; i < _freeHandles.Count; i++)
                {
                    var curDistance = Vector2.Distance(fingerPos, _freeHandles[i].transform.position); 
                    if (curDistance < minDistance)
                    {
                        minDistance = curDistance;
                        nearestHandleIndex = i;
                    }
                }
            }
            var handle = _freeHandles[nearestHandleIndex];
            _freeHandles.Remove(handle);
            return handle;
        }

        private void OnFingerUp(Finger finger)
        {
            lock (Lock)
            {
                if (!_pairings.ContainsKey(finger)) return;
                var handle = _pairings[finger];
                handle.finger = null;
                _pairings.Remove(finger);
                _freeHandles.Add(handle);
                if(_pairings.Count == 0) OnInputLost?.Invoke();
            }
        }

        private void OnPlayerDead() => FreeAllFingers();

        private void FreeAllFingers()
        {
            var fingers = _pairings.Keys.ToArray();
            for (int i = 0; i < fingers.Length; i++)
            {
                OnFingerUp(fingers[i]);
            }
        }
    }
}