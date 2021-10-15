using UnityEngine;

namespace FingerFighter.View
{
    public class JumpObjectOnOutOfCamera : MonoBehaviour
    {
        [SerializeField] private Vector2 jumpDirection = new Vector2(0, 20);

        private void OnTriggerExit2D(Collider2D other)
        {
            transform.position += (Vector3) jumpDirection;
        }
    }
}