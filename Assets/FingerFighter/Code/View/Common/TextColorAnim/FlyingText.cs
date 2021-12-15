using FingerFighter.Control.Common.Factories;
using FingerFighter.Model.ViewData;
using TMPro;
using UnityEngine;

namespace FingerFighter.View.Common.TextColorAnim
{
    [RequireComponent(typeof(TextMeshPro))]
    public class FlyingText : TmpTextFade
    {
        [SerializeField] private float speed = 1f;

        private Vector2 _direction;
        
        public void Init(FlyingTextData data)
        {
            transform.position = data.Position;
            _direction = data.Direction;
            SetText(data.Text);
            SetTextColor(data.TextColor);
            ResetDurationTimer();
        }

        protected override void Update()
        {
            base.Update();
            Move();
            RepoolIfNeeded();
        }

        private void Move() 
            => transform.position = (Vector2) transform.position + _direction * (speed * Time.deltaTime);
        
        private void RepoolIfNeeded()
        {
            if (ShowTimeIsOver)
            {
                FlyingTextFactory.Instance.ReturnToPool(this);
            }
        }
    }
}
