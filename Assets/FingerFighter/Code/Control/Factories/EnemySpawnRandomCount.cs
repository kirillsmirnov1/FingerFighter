using System.Collections.Generic;
using FingerFighter.Model.Combat;
using FingerFighter.View;
using UnityEngine;

namespace FingerFighter.Control.Factories
{
    public class EnemySpawnRandomCount : JumpObjectOnOutOfCamera
    {
        [SerializeField] private Vector2Int enemyCount = new Vector2Int(3, 10);
        [SerializeField] private EnemyPrefabs enemies;
        
        private readonly Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();
        private readonly Dictionary<string, Queue<GameObject>> _pool = new Dictionary<string, Queue<GameObject>>();
        
        private Transform _anchor;

        private static EnemySpawnRandomCount _instance;

        private void Awake()
        {
            _instance = this;
            _anchor = transform.parent;
            InitDictionaries();
        }

        private void InitDictionaries()
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                _pool.Add(enemies[i].tag, new Queue<GameObject>());
                _prefabs.Add(enemies[i].tag, enemies[i].prefab);
            }
        }

        protected override void OnTriggerExit2D(Collider2D other) { }

        private void OnTriggerEnter2D(Collider2D other)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                SpawnEnemies(enemies[i].tag);
            }
            Jump();
        }

        private void SpawnEnemies(string enemyTag)
        {
            var count = Random.Range(enemyCount.x, enemyCount.y);
            Vector2 curPos = transform.position;
            for (int i = 0; i < count; i++)
            {
                var pos = curPos + Random.insideUnitCircle * jumpDirection.y / 2;
                var instance = GetFromPool(enemyTag);
                instance.SetActive(true);
                instance.transform.position = pos;
            }
        }

        private GameObject GetFromPool(string enemyTag)
        {
            return _pool[enemyTag].Count > 0 
                ? _pool[enemyTag].Dequeue() 
                : Instantiate(_prefabs[enemyTag], _anchor);
        }

        public static void ReturnToPool(GameObject obj, string enemyType)
        {
            _instance?._pool[enemyType].Enqueue(obj);
        }
    }
}