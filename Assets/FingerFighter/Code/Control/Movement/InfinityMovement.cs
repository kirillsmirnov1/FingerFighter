using UnityEngine;

namespace FingerFighter.Control.Movement
{
    public class InfinityMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private float radius = 1f;
        [SerializeField] private Rigidbody2D rb;
        
        private float _t;

        private void Update()
        {
            _t += Time.deltaTime;
            var t = speed * _t;
            rb.MovePosition(radius * new Vector2(Mathf.Sin(2 * t) / 2, Mathf.Cos(t)));
        }
    }
}