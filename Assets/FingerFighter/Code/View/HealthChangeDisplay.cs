using FingerFighter.Control.Combat.Health;
using TMPro;
using UnityEngine;

namespace FingerFighter.View
{
    [RequireComponent(typeof(TextMeshPro))]
    public class HealthChangeDisplay : MonoBehaviour
    {
        [SerializeField] private AHealth health;
        [SerializeField] private TextMeshPro text;
        [SerializeField] private float duration = 1f;
        [SerializeField] private AnimationCurve textTransparency;

        private float _durationLeft;
        
        private void Awake()
        {
            health.onHealthChange += DisplayHealthChange;
        }

        private void OnEnable()
        {
            SetTextAlpha(0f);
            _durationLeft = 0f;
        }

        private void Update()
        {
            UpdateTextAlpha();
        }

        private void UpdateTextAlpha()
        {
            if (_durationLeft < 0.001f) return;
            transform.rotation = Quaternion.identity;
            _durationLeft -= Time.deltaTime;
            var t = 1 - _durationLeft / duration;
            var textAlpha = textTransparency.Evaluate(t);
            SetTextAlpha(textAlpha);
        }

        private void DisplayHealthChange(float currHealth)
        {
            var healthPercent = (int) (100 * currHealth / health.BaseHealth);
            text.text = $"♥{healthPercent}%";
            _durationLeft = duration;
        }

        private void SetTextAlpha(float alpha)
        {
            var color = text.color;
            color.a = alpha;
            text.color = color;
        }
    }
}