using System.Collections.Generic;
using FingerFighter.Model.Common.Combat;
using FingerFighter.Model.Common.Enemies;
using Unity.Mathematics;
using UnityEngine;

namespace FingerFighter.Control.Common.Factories
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] private EnemyStatsList enemyData;
        
        private readonly Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();
        private readonly Dictionary<string, Queue<EnemyComponents>> _pool = new Dictionary<string, Queue<EnemyComponents>>();
        
        private Transform _anchor;
        private static EnemyPool _instance;

        private void Awake()
        {
            _instance = this;
            _anchor = transform;
            InitDictionaries();
        }

        private void InitDictionaries()
        {
            for (var i = 0; i < enemyData.Count; i++)
            {
                _pool.Add(enemyData[i].tag, new Queue<EnemyComponents>());
                _prefabs.Add(enemyData[i].tag, enemyData[i].prefab);
            }
        }

        public static EnemyComponents Get(string enemyTag, Vector2 spawnPos = new Vector2(), float rotation = 0f) 
            => _instance.GetImpl(enemyTag, spawnPos, rotation);

        private EnemyComponents GetImpl(string enemyTag, Vector2 spawnPos = new Vector2(), float rotation = 0f)
        {
            var obj = _pool[enemyTag].Count > 0 
                ? _pool[enemyTag].Dequeue() 
                : Instantiate(_prefabs[enemyTag], _anchor).GetComponent<EnemyComponents>();

            obj.transform.position = spawnPos;
            if (rotation != 0f)
                obj.transform.rotation = quaternion.Euler(0, 0, rotation);

            return obj;
        }

        public static void ReturnToPool(EnemyComponents obj, string enemyType) 
            => _instance.ReturnToPoolImpl(obj, enemyType);

        private void ReturnToPoolImpl(EnemyComponents obj, string enemyType) 
            => _pool[enemyType].Enqueue(obj);
    }
}