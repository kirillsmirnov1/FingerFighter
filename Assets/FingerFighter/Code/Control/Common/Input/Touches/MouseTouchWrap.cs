using UnityEngine;

namespace FingerFighter.Control.Common.Input.Touches
{
    public class MouseTouchWrap : ITouchWrap
    {
        public Vector2 screenPosition => UnityEngine.Input.mousePosition;
    }
}