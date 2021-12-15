using UnityEngine;

namespace FingerFighter.Control.Common.Factories.EnemySpawn
{
    public abstract class AEnemySpawn : MonoBehaviour
    {
        protected Vector2 CurrentPos;

        protected virtual void Awake() { }

        protected void SpawnEnemy(string enemyTag, Vector2 relativePos)
        {
            var pos = CurrentPos + relativePos;
            var instance = EnemyPool.Get(enemyTag, pos);
            instance.gameObject.SetActive(true);
        }
    }
}