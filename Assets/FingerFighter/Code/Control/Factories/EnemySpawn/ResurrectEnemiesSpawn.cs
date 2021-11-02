using UnityEngine;
using UnityUtils.Extensions;

namespace FingerFighter.Control.Factories.EnemySpawn
{
    public class ResurrectEnemiesSpawn : AEnemySpawn
    {
        [Header("Ring Spawn")]
        [SerializeField] private float respawnDelay = 3f;

        protected override void ReturnToPoolImpl(GameObject obj, string enemyType)
        {
            if(!gameObject.activeSelf) return;
            base.ReturnToPoolImpl(obj, enemyType);
            this.DelayAction(respawnDelay, () => SpawnEnemy(enemyType, obj.transform.position));
        }

        protected override void Spawn() { }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            // No need to react to camera on ring 
        }
    }
}