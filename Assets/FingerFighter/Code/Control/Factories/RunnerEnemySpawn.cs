using FingerFighter.View;
using UnityEngine;

namespace FingerFighter.Control.Factories
{
    // TODO add type to CombatantId 
    // TODO pool by type
    public class RunnerEnemySpawn : JumpObjectOnOutOfCamera
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Vector2Int enemyCount = new Vector2Int(3, 10);

        private Transform _anchor;
        
        protected override void OnTriggerExit2D(Collider2D other) { }

        private void OnTriggerEnter2D(Collider2D other)
        {
            SpawnEnemies();
            Jump();
        }

        private void Awake()
        {
            _anchor = transform.parent;
        }

        private void SpawnEnemies()
        {
            var count = Random.Range(enemyCount.x, enemyCount.y);
            Vector2 curPos = transform.position;
            for (int i = 0; i < count; i++)
            {
                var pos = curPos + Random.insideUnitCircle * jumpDirection.y / 2;
                Instantiate(enemyPrefab, pos, Quaternion.identity, _anchor);
            }
        }
    }
}