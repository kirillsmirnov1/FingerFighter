using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace FingerFighter.Sandbox
{
    public class Handle : MonoBehaviour
    {
        [HideInInspector] public Finger finger;

        private void Update()
        {
            // IMPR state machine 
            if(finger == null) return;
            var fingerPos = Camera.main.ScreenToWorldPoint(finger.screenPosition);
            fingerPos.z = 0;
            transform.position = fingerPos; // TODO use rb 
        }
    }
}
