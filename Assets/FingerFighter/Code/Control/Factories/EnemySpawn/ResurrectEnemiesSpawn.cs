using FingerFighter.Control.Combat.Status;
using FingerFighter.Model.Combat;
using UnityEngine;
using UnityUtils.Extensions;

namespace FingerFighter.Control.Factories.EnemySpawn
{
    public class ResurrectEnemiesSpawn : AEnemySpawn
    {
        [Header("Ring Spawn")]
        [SerializeField] private float respawnDelay = 3f;

        protected override void Awake()
        {
            base.Awake();
            EnemyStatus.OnDeath += OnEnemyDeath;
        }

        private void OnDestroy() 
            => EnemyStatus.OnDeath -= OnEnemyDeath;

        private void OnEnemyDeath(EnemyDeathData deathData)
        {
            if(deathData.IsProjectile) return;
            if (!gameObject.activeSelf) return;
            this.DelayAction(respawnDelay, () => SpawnEnemy(deathData.Tag, deathData.DeathPos));
        }

        protected override void Spawn() { }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            // No need to react to camera on ring 
        }
    }
}