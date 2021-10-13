using TMPro;
using UnityEngine;

namespace FingerFighter.Sandbox
{
    [RequireComponent(typeof(TextMeshPro))]
    public class HitDamageText : MonoBehaviour
    {
        [SerializeField] private TextMeshPro text;
        [SerializeField] private float duration = 3f;
        [SerializeField] private float speed = 1f;
        
        private Vector2 _direction;
        
        public void Init(float hitForce, Vector2 direction)
        {
            text.text = $"{(int) hitForce}";
            _direction = direction.normalized;
        }

        private void Update()
        {
            transform.position = (Vector2)transform.position + _direction * (speed * Time.deltaTime);
            duration -= Time.deltaTime;
            if(duration <= 0) Destroy(gameObject);
        }
    }
}
