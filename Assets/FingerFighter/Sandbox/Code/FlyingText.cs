using TMPro;
using UnityEngine;

namespace FingerFighter.Sandbox
{
    [RequireComponent(typeof(TextMeshPro))]
    public class FlyingText : MonoBehaviour
    {
        [SerializeField] private TextMeshPro text;
        [SerializeField] private float duration = 3f;
        [SerializeField] private float speed = 1f;
        
        private Vector2 _direction;
        private float _durationLeft;
        
        public void Init(string textToShow, Vector2 direction)
        {
            text.text = textToShow;
            _direction = direction;
            _durationLeft = duration;
        }

        private void Update()
        {
            Move();
            SubtractTime();
            RepoolIfNeeded();
        }

        private void Move() 
            => transform.position = (Vector2) transform.position + _direction * (speed * Time.deltaTime);

        private void SubtractTime() 
            => _durationLeft -= Time.deltaTime;

        private void RepoolIfNeeded()
        {
            if (_durationLeft <= 0)
            {
                FlyingTextFactory.Instance.ReturnToPool(this);
            }
        }
    }
}
