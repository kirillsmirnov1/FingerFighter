using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace FingerFighter.Sandbox
{
    public class Handle : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        
        [HideInInspector] public Finger finger;

        private void FixedUpdate()
        {
            // IMPR state machine 
            if(finger == null) return;
            
            Vector2 fingerPos = Camera.main.ScreenToWorldPoint(finger.screenPosition);
            
            rb.MovePosition(fingerPos);
        }
    }
}
