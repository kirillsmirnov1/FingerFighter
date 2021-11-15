using UnityEngine;

namespace FingerFighter.Model.Combat.Damage.HitDataProvider
{
    public class EnemyCollisionHitForce : AHitDataProvider
    {
        [SerializeField] private CombatEntityId id;
        private HitData _hitData;

        private void OnValidate()
        {
            id = GetComponent<CombatEntityId>();
        }

        private void OnEnable()
        {
            _hitData = new HitData
            {
                Force = id.EnemyStats.collisionDamage
            };
        }

        public override HitData HitData => _hitData;
    }
}