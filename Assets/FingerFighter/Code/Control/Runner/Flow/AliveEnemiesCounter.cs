using System;
using FingerFighter.Control.Common.Combat.Status;
using FingerFighter.Model.Common.Combat;
using UnityEngine;

namespace FingerFighter.Control.Runner.Flow
{
    public class AliveEnemiesCounter : MonoBehaviour
    {
        public static event Action OnNoEnemiesLeftAlive; 
        private uint _aliveEnemies;
        
        private void Awake()
        {
            EnemyStatus.OnSpawn += OnEnemySpawn;
            EnemyStatus.OnDeath += OnEnemyDeath;
        }

        private void OnDestroy()
        {
            EnemyStatus.OnSpawn -= OnEnemySpawn;
            EnemyStatus.OnDeath -= OnEnemyDeath;
        }

        private void OnEnemySpawn() 
            => _aliveEnemies++;

        private void OnEnemyDeath(EnemyDeathData obj)
        {
            _aliveEnemies--;
            if (_aliveEnemies == 0)
            {
                OnNoEnemiesLeftAlive?.Invoke();
            }
        }
    }
}