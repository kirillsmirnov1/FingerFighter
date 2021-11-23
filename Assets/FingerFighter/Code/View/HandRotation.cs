using FingerFighter.Control.Input.Handles;
using UnityEngine;

namespace FingerFighter.View
{
    public class HandRotation : MonoBehaviour
    {
        [SerializeField] private Transform body;
        [SerializeField] private HandleSpeed speed;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float rotationSpeed = 30f;
        
        private void Update()
        {
            rb.angularVelocity = 0f;
            var movementDirection = speed.Direction;
            
            // too rough [0.1f; 0.01f] too sensitive
            if(movementDirection.magnitude < 0.05f) return; 

            Vector2 toBody = body.position - transform.position;
            if (Vector2.Dot(movementDirection, toBody) > 0) movementDirection *= -1f;
            
            transform.up = Vector2.Lerp(transform.up, movementDirection, rotationSpeed * Time.deltaTime); 
        }
    }
}