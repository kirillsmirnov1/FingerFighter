using System;
using FingerFighter.Control.Combat.Status;
using FingerFighter.Model.Combat;
using UnityEngine;

namespace FingerFighter.Control.Combat.Flow
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