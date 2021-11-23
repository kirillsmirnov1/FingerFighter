using TMPro;
using UnityEngine;

namespace FingerFighter.View.TextColorAnim
{
    [RequireComponent(typeof(TMP_Text))]
    public class TmpTextFade : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private float duration = 1;
        [SerializeField] private AnimationCurve textAlpha = AnimationCurve.Linear(0, 1, 1, 0);

        private float _durationLeft;

        protected bool ShowTimeIsOver => _durationLeft < 0.00001f;

        protected virtual void OnValidate() 
            => text = GetComponent<TMP_Text>();

        private void OnEnable()
        {
            SetTextAlpha(0f);
            _durationLeft = 0f;
        }

        protected virtual void Update() 
            => UpdateTextAlpha();

        private void UpdateTextAlpha()
        {
            if (ShowTimeIsOver) return;
            transform.rotation = Quaternion.identity;
            _durationLeft -= Time.deltaTime;
            var t = 1 - _durationLeft / duration;
            var alpha = textAlpha.Evaluate(t);
            SetTextAlpha(alpha);
        }

        protected void ResetDurationTimer() 
            => _durationLeft = duration;

        private void SetTextAlpha(float alpha)
        {
            var color = text.color;
            color.a = alpha;
            text.color = color;
        }

        protected void SetText(string newText) 
            => text.text = newText;

        protected void SetTextColor(Color color)
            => text.color = color;
    }
}