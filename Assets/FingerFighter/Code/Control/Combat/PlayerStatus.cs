using System;

namespace FingerFighter.Control.Combat
{
    public class PlayerStatus : CombatEntityStatus
    {
        public static event Action OnAlive;
        public static event Action OnDead;
        protected override void OnEnable()
        {
            base.OnEnable();
            OnAlive?.Invoke();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            OnDead?.Invoke();
        }
    }
}