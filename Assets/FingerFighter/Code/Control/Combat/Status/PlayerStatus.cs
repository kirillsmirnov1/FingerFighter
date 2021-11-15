using System;

namespace FingerFighter.Control.Combat.Status
{
    public class PlayerStatus : CombatEntityStatus
    {
        public static event Action OnAlive;
        public static event Action OnDeath;
        protected override void OnEnable()
        {
            base.OnEnable();
            OnAlive?.Invoke();
        }

        protected override void OnEntityDeath() => OnDeath?.Invoke();
    }
}