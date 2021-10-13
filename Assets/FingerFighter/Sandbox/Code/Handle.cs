using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace FingerFighter.Sandbox
{
    public class Handle : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Transform body;
        [SerializeField] private float handLength = 2f;
        
        [HideInInspector] public Finger finger;

        private void FixedUpdate()
        {
            // IMPR state machine 
            if(finger == null) return;
            
            Vector2 fingerPos = Camera.main.ScreenToWorldPoint(finger.screenPosition);
            Vector2 bodyPos = body.position; 
            
            var toFinger = fingerPos - bodyPos;
            var toFingerClamped = Vector2.ClampMagnitude(toFinger, handLength);
            
            var targetPos = bodyPos + toFingerClamped;
            
            rb.MovePosition(targetPos);
        }
    }
}
