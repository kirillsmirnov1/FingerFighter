using System;
using FingerFighter.Control.Factories.EnemySpawn;
using UnityEngine;

namespace FingerFighter.Control.Combat
{
    public class EnemyStatus : CombatEntityStatus
    {
        /// <summary>
        /// tag and position
        /// </summary>
        public static event Action<string, Vector2> OnDeath;
        
        protected override void OnDisable()
        {
            base.OnDisable();
            OnDeath?.Invoke(id.EnemyType, transform.position);
            AEnemySpawn.ReturnToPool(gameObject, id.EnemyType);
        }
    }
}