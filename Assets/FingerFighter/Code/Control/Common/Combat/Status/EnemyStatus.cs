using System;
using FingerFighter.Control.Common.Factories;
using FingerFighter.Model.Common.Combat;
using FingerFighter.Model.Common.Enemies;
using UnityEngine;

namespace FingerFighter.Control.Common.Combat.Status
{
    [RequireComponent(typeof(EnemyComponents))]
    public class EnemyStatus : CombatEntityStatus
    {
        [SerializeField] private bool isSegment;
        [SerializeField] private bool isProjectile;
        [SerializeField] public Transform deathPosTransform;
        [SerializeField] private EnemyComponents components;

        public static event Action OnSpawn;
        /// <summary>
        /// tag, segment flag and position
        /// </summary>
        public static event Action<EnemyDeathData> OnDeath;

        protected override void OnValidate()
        {
            base.OnValidate();
            components ??= GetComponent<EnemyComponents>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if(isSegment || isProjectile) return;
            OnSpawn?.Invoke();
        }

        protected override void OnEntityDeath()
        {
            if(isSegment) return;
            EnemyPool.ReturnToPool(components, components.EnemyType);
            if(isProjectile) return;
            OnDeath?.Invoke(DeathData);
        }

        private EnemyDeathData DeathData =>
            new EnemyDeathData
            {
                Tag = components.EnemyType, 
                IsSegment = isSegment, 
                IsProjectile = isProjectile,
                DeathPos = deathPosTransform.position
            };
    }
}