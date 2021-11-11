using System;
using FingerFighter.Control.Factories.EnemySpawn;
using FingerFighter.Model.Combat;
using UnityEngine;

namespace FingerFighter.Control.Combat
{
    public class EnemyStatus : CombatEntityStatus
    {
        [SerializeField] private bool isSegment;
        [SerializeField] private Transform deathPosTransform;
        
        /// <summary>
        /// tag, segment flag and position
        /// </summary>
        public static event Action<EnemyDeathData> OnDeath;
        
        protected override void OnEntityDeath()
        {
            OnDeath?.Invoke(DeathData);
            if (!isSegment)
            {
                AEnemySpawn.ReturnToPool(gameObject, id.EnemyType);
            }
        }

        private EnemyDeathData DeathData =>
            new EnemyDeathData
            {
                Tag = id.EnemyType, 
                IsSegment = isSegment, 
                DeathPos = deathPosTransform.position
            };
    }
}