using System;
using FingerFighter.Control.Factories.EnemySpawn;
using FingerFighter.Model.Combat;
using UnityEngine;

namespace FingerFighter.Control.Combat
{
    public class EnemyStatus : CombatEntityStatus
    {
        [SerializeField] private bool isSegment;
        
        /// <summary>
        /// tag, segment flag and position
        /// </summary>
        public static event Action<EnemyDeathData> OnDeath;
        
        protected override void OnDisable()
        {
            base.OnDisable();
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
                DeathPos = transform.position
            };
    }
}