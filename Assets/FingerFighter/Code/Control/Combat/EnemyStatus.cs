using System;
using FingerFighter.Control.Factories.EnemySpawn;
using UnityEngine;

namespace FingerFighter.Control.Combat
{
    public class EnemyStatus : CombatEntityStatus
    {
        [SerializeField] private bool isSegment;
        
        /// <summary>
        /// tag, segment flag and position
        /// </summary>
        public static event Action<string, bool, Vector2> OnDeath;
        
        protected override void OnDisable()
        {
            base.OnDisable();
            OnDeath?.Invoke(id.EnemyType, isSegment, transform.position);
            if (!isSegment)
            {
                AEnemySpawn.ReturnToPool(gameObject, id.EnemyType);
            }
        }
    }
}