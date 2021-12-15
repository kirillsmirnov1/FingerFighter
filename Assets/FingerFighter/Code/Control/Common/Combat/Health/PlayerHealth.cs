using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Control.Common.Combat.Health
{
    public class PlayerHealth : AHealth
    {
        [SerializeField] private FloatVariable baseHealth;
        [SerializeField] private FloatVariable currentHealth;

        protected override void OnEnable() { }

        public override float BaseHealth => baseHealth;
        public override float CurrentHealth
        {
            get => currentHealth;
            protected set => currentHealth.Value = value;
        }
    }
}
