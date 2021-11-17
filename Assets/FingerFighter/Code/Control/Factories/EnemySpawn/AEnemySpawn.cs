using FingerFighter.Control.Cam;
using FingerFighter.Model.Combat;
using UnityEngine;

namespace FingerFighter.Control.Factories.EnemySpawn
{
    public abstract class AEnemySpawn : JumpObjectOnOutOfCamera
    {
        [SerializeField] protected EnemyStatsList enemyData;
        
        protected Vector2 CurrentPos;

        protected virtual void Awake() { }

        protected override void OnTriggerExit2D(Collider2D other) { }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            CurrentPos = transform.position;
            Spawn();
            Jump();
        }

        protected abstract void Spawn();

        protected void SpawnEnemy(string enemyTag, Vector2 relativePos)
        {
            var pos = CurrentPos + relativePos;
            var instance = EnemyPool.Get(enemyTag, pos);
            instance.SetActive(true);
        }
    }
}