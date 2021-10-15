using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace FingerFighter.Control.Character.Handles
{
    [DefaultExecutionOrder(-1)]
    public class HandlesInputManager : MonoBehaviour
    {
        [SerializeField] private Handle[] handles;

        private readonly Dictionary<Finger, Handle> _pairings = new Dictionary<Finger, Handle>();
        private Queue<Handle> _freeHandles;

        private static readonly object Lock = new object(); 
    
        private void OnValidate()
        {
            handles = GetComponentsInChildren<Handle>();
        }

        private void Awake()
        {
            _freeHandles = new Queue<Handle>(handles);
            EnhancedTouchSupport.Enable();
        }

        private void OnEnable()
        {
            Touch.onFingerDown += OnFingerDown;
            Touch.onFingerUp += OnFingerUp;
        }

        private void OnDisable()
        {
            Touch.onFingerDown -= OnFingerDown;
            Touch.onFingerUp -= OnFingerUp;
        }

        private void OnFingerDown(Finger finger)
        {
            lock (Lock)
            {
                if (_freeHandles.Count <= 0) return;
                var handle = _freeHandles.Dequeue(); // TODO pick nearest 
                handle.finger = finger;
                _pairings.Add(finger, handle);
            }
        }

        private void OnFingerUp(Finger finger)
        {
            lock (Lock)
            {
                if (!_pairings.ContainsKey(finger)) return;
                var handle = _pairings[finger];
                handle.finger = null;
                _pairings.Remove(finger);
                _freeHandles.Enqueue(handle);
            }
        }
    }
}