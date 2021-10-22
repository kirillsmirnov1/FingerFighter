using System.Collections.Generic;
using FingerFighter.Model.Combat;
using FingerFighter.View;
using UnityEngine;

namespace FingerFighter.Control.Factories
{
    public abstract class EnemySpawn : JumpObjectOnOutOfCamera
    {
        [SerializeField] protected EnemyStatsList enemyData;
        
        private readonly Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
        private readonly Dictionary<string, Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();
        
        private Transform anchor;
        private static EnemySpawn _instance;
        private Vector2 _currentPos;

        private void Awake()
        {
            _instance = this;
            anchor = transform.parent;
            InitDictionaries();
        }

        private void InitDictionaries()
        {
            for (int i = 0; i < enemyData.Count; i++)
            {
                pool.Add(enemyData[i].tag, new Queue<GameObject>());
                prefabs.Add(enemyData[i].tag, enemyData[i].prefab);
            }
        }

        protected override void OnTriggerExit2D(Collider2D other) { }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _currentPos = transform.position;
            Spawn();
            Jump();
        }

        protected abstract void Spawn();

        protected void SpawnEnemy(string enemyTag, Vector2 relativePos)
        {
            var pos = _currentPos + relativePos;
            var instance = GetFromPool(enemyTag);
            instance.SetActive(true);
            instance.transform.position = pos;
        }

        protected GameObject GetFromPool(string enemyTag)
        {
            return pool[enemyTag].Count > 0 
                ? pool[enemyTag].Dequeue() 
                : Instantiate(prefabs[enemyTag], anchor);
        }

        public static void ReturnToPool(GameObject obj, string enemyType)
        {
            _instance?.pool[enemyType].Enqueue(obj);
        }
    }
}