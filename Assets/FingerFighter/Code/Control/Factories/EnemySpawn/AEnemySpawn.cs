using FingerFighter.Control.Cam;
using FingerFighter.Model.Combat;
using UnityEngine;

namespace FingerFighter.Control.Factories.EnemySpawn
{
    public abstract class AEnemySpawn : JumpObjectOnOutOfCamera
    {
        [SerializeField] protected EnemyStatsList enemyData;
        
        private Vector2 _currentPos;

        protected virtual void Awake() { }

        protected override void OnTriggerExit2D(Collider2D other) { }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            _currentPos = transform.position;
            Spawn();
            Jump();
        }

        protected abstract void Spawn();

        protected void SpawnEnemy(string enemyTag, Vector2 relativePos)
        {
            var pos = _currentPos + relativePos;
            var instance = EnemyPool.Get(enemyTag);
            instance.SetActive(true);
            instance.transform.position = pos;
        }
    }
}