using FingerFighter.Model.Combat;
using UnityEngine;
using UnityUtils.Extensions;

namespace FingerFighter.Control.Factories.EnemySpawn
{
    public class RingEnemySpawn : AEnemySpawn
    {
        [Header("Ring Spawn")]
        [SerializeField] private EnemyStats dummyStats;
        [SerializeField] private Vector2 dummyPos;
        [SerializeField] private float respawnDelay = 3f;

        protected override void ReturnToPoolImpl(GameObject obj, string enemyType)
        {
            base.ReturnToPoolImpl(obj, enemyType);
            this.DelayAction(respawnDelay, Spawn);
        }

        protected override void Spawn()
        {
            SpawnEnemy(dummyStats.tag, dummyPos);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            // No need to react to camera on ring 
        }
    }
}