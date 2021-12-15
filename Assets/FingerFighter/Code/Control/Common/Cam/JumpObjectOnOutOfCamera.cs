using UnityEngine;

namespace FingerFighter.Control.Common.Cam
{
    public class JumpObjectOnOutOfCamera : MonoBehaviour
    {
        [SerializeField] protected Vector2 jumpDirection = new Vector2(0, 20); 

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            Jump();
        }

        protected void Jump()
        {
            transform.position += (Vector3) jumpDirection;
        }
    }
}