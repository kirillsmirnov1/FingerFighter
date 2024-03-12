using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace FingerFighter.Control.Common.Input.Touches
{
    public class FingerTouchWrap : ITouchWrap
    {
        private Finger _finger;
        public FingerTouchWrap(Finger finger)
        {
            _finger = finger;
        }

        public Vector2 screenPosition => _finger.screenPosition;
    }
}