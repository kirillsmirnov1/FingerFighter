using FingerFighter.Control.Combat.Health;
using TMPro;
using UnityEngine;

namespace FingerFighter.View.TextColorAnim
{
    [RequireComponent(typeof(TextMeshPro))]
    public class HealthChangeDisplay : TmpTextFade
    {
        [SerializeField] private AHealth health;

        protected override void OnValidate()
        {
            base.OnValidate();
            health ??= GetComponentInParent<AHealth>();
        }

        protected void Awake()
        {
            health.onHealthChange += DisplayHealthChange;
        }

        private void DisplayHealthChange(float currHealth)
        {
            var healthPercent = (int) (100 * currHealth / health.BaseHealth);
            SetText($"♥{healthPercent}%");
            ResetDurationTimer();
        }
    }
}