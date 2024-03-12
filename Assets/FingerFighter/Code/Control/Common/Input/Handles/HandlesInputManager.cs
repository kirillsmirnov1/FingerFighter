using System;
using System.Collections.Generic;
using System.Linq;
using FingerFighter.Control.Common.Combat.Status;
using FingerFighter.Control.Common.Input.Touches;
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

        private readonly Dictionary<ITouchWrap, Handle> _pairings = new Dictionary<ITouchWrap, Handle>();
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

#if UNITY_WEBGL

        private readonly ITouchWrap _mouseTouchWrap = new MouseTouchWrap();
        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                OnTouchDown(_mouseTouchWrap);
            }
            else if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                OnTouchUp(_mouseTouchWrap);
            }
        }

#endif

        private void OnFingerDown(Finger finger)
        {
            OnTouchDown(new FingerTouchWrap(finger));
        }

        private void OnTouchDown(ITouchWrap touchWrap)
        {
            lock (Lock)
            {
                if (_freeHandles.Count <= 0) return;
                if (IsPointerOverUiComponent<Image>(touchWrap.screenPosition)) return;
                var handle = PickHandleForFinger(touchWrap);  
                handle.touchWrap = touchWrap;
                _pairings.Add(touchWrap, handle);
                if(_pairings.Count == 1) OnInputRegained?.Invoke();
            }
        }

        private Handle PickHandleForFinger(ITouchWrap touchWrap)
        {
            var nearestHandleIndex = 0;
            if (_freeHandles.Count > 1)
            {
                var fingerPos = _camera.ScreenToWorldPoint(touchWrap.screenPosition);
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
            => OnTouchUp(new FingerTouchWrap(finger));
        
        private void OnTouchUp(ITouchWrap touchWrap)
        {
            lock (Lock)
            {
                if (!_pairings.ContainsKey(touchWrap)) return;
                var handle = _pairings[touchWrap];
                handle.touchWrap = null;
                _pairings.Remove(touchWrap);
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
                OnTouchUp(fingers[i]);
            }
        }

        private static bool IsPointerOverUiComponent<T>(Vector2 pos)
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = pos
            };

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            return results.Any(r => r.gameObject.TryGetComponent<T>(out _));
        }
    }
}