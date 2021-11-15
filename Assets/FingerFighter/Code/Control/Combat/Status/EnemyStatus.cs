using System;
using FingerFighter.Control.Factories;
using FingerFighter.Model.Combat;
using UnityEngine;

namespace FingerFighter.Control.Combat.Status
{
    public class EnemyStatus : CombatEntityStatus
    {
        [SerializeField] private bool isSegment;
        [SerializeField] private bool isProjectile;
        [SerializeField] private Transform deathPosTransform;

        public static event Action OnSpawn;
        /// <summary>
        /// tag, segment flag and position
        /// </summary>
        public static event Action<EnemyDeathData> OnDeath;

        protected override void OnEnable()
        {
            base.OnEnable();
            if(isSegment || isProjectile) return;
            OnSpawn?.Invoke();
        }

        protected override void OnEntityDeath()
        {
            if(isSegment) return;
            EnemyPool.ReturnToPool(gameObject, id.EnemyType);
            if(isProjectile) return;
            OnDeath?.Invoke(DeathData);
        }

        private EnemyDeathData DeathData =>
            new EnemyDeathData
            {
                Tag = id.EnemyType, 
                IsSegment = isSegment, 
                IsProjectile = isProjectile,
                DeathPos = deathPosTransform.position
            };
    }
}