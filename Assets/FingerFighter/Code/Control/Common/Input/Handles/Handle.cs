using FingerFighter.Control.Common.Input.Touches;
using UnityEngine;

namespace FingerFighter.Control.Common.Input.Handles
{
    public class Handle : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Transform body;
        [SerializeField] private float handLength = 2f;
        
        [HideInInspector] public ITouchWrap touchWrap;

        private UnityEngine.Camera _camera;
        
        private void OnEnable()
        {
            _camera = UnityEngine.Camera.main;
        }

        private void Update()
        {
            if(touchWrap == null) return;
            
            Vector2 fingerPos = _camera.ScreenToWorldPoint(touchWrap.screenPosition);
            Vector2 bodyPos = body.position; 
            
            var toFinger = fingerPos - bodyPos;
            var toFingerClamped = Vector2.ClampMagnitude(toFinger, handLength);
            
            var targetPos = bodyPos + toFingerClamped;
            
            rb.MovePosition(targetPos);
        }
    }
}
