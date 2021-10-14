using FingerFighter.Control.Factories;
using TMPro;
using UnityEngine;

namespace FingerFighter.View
{
    [RequireComponent(typeof(TextMeshPro))]
    public class FlyingText : MonoBehaviour
    {
        [SerializeField] private TextMeshPro text;
        [SerializeField] private float duration = 3f;
        [SerializeField] private float speed = 1f;
        [SerializeField] private AnimationCurve textAlphaCurve;
        
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
            UpdateTextAlpha();
            SubtractTime();
            RepoolIfNeeded();
        }

        private void Move() 
            => transform.position = (Vector2) transform.position + _direction * (speed * Time.deltaTime);

        private void UpdateTextAlpha()
        {
            var color = text.color;
            var t = 1 - _durationLeft / duration; 
            color.a = textAlphaCurve.Evaluate(t);
            text.color = color;
        }

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
