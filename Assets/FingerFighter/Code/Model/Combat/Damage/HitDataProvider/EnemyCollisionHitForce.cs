using FingerFighter.Model.Enemies;
using UnityEngine;

namespace FingerFighter.Model.Combat.Damage.HitDataProvider
{
    public class EnemyCollisionHitForce : AHitDataProvider
    {
        [SerializeField] private EnemyComponents components;
        private HitData _hitData;

        private void OnValidate()
        {
            components = GetComponent<EnemyComponents>();
        }

        private void OnEnable()
        {
            _hitData = new HitData
            {
                Force = components.stats.collisionDamage
            };
        }

        public override HitData HitData => _hitData;
    }
}