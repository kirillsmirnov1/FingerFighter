using UnityEngine;

namespace FingerFighter.Control.Movement
{
    public class InfinityMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private float radius = 1f;
        
        private float _t;

        private void Update()
        {
            _t += Time.deltaTime;
            var t = speed * _t;
            transform.position = radius 
                * new Vector2(Mathf.Sin(2 * t) / 2, Mathf.Cos(t));
        }
    }
}