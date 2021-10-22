using UnityEngine;

namespace FingerFighter.Control.Factories.EnemySpawn
{
    public class EnemySpawnRandomCount : AEnemySpawn
    {
        [SerializeField] private Vector2Int enemyCount = new Vector2Int(3, 10);

        protected override void Spawn()
        {
            for (int i = 0; i < enemyData.Count; i++)
            {
                SpawnEnemies(enemyData[i].tag);
            }
        }

        private void SpawnEnemies(string enemyTag)
        {
            var count = Random.Range(enemyCount.x, enemyCount.y);
            for (int i = 0; i < count; i++)
            {
                var relativePos = Random.insideUnitCircle * jumpDirection.y / 2;
                SpawnEnemy(enemyTag, relativePos);
            }
        }
    }
}