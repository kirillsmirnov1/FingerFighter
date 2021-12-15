using FingerFighter.Control.Common.Combat.Health;
using TMPro;
using UnityEngine;

namespace FingerFighter.View.Common.TextColorAnim
{
    [RequireComponent(typeof(TextMeshPro))]
    public class HealthChangeDisplay : TmpTextFade
    {
        [SerializeField] private AHealth health;
        [SerializeField] private string prefix = "<sprite index=0 tint=1>";
        [SerializeField] private string postfix = "%";
        
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
            SetText($"{prefix}{healthPercent}{postfix}");
            ResetDurationTimer();
        }
    }
}