using System.Collections.Generic;
using FingerFighter.Model.Combat;
using Unity.Mathematics;
using UnityEngine;

namespace FingerFighter.Control.Factories
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] private EnemyStatsList enemyData;
        
        private readonly Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();
        private readonly Dictionary<string, Queue<CombatEntityId>> _pool = new Dictionary<string, Queue<CombatEntityId>>();
        
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
                _pool.Add(enemyData[i].tag, new Queue<CombatEntityId>());
                _prefabs.Add(enemyData[i].tag, enemyData[i].prefab);
            }
        }

        public static CombatEntityId Get(string enemyTag, Vector2 spawnPos = new Vector2(), float rotation = 0f) 
            => _instance.GetImpl(enemyTag, spawnPos, rotation);

        private CombatEntityId GetImpl(string enemyTag, Vector2 spawnPos = new Vector2(), float rotation = 0f)
        {
            var obj = _pool[enemyTag].Count > 0 
                ? _pool[enemyTag].Dequeue() 
                : Instantiate(_prefabs[enemyTag], _anchor).GetComponent<CombatEntityId>();

            obj.transform.position = spawnPos;
            if (rotation != 0f)
                obj.transform.rotation = quaternion.Euler(0, 0, rotation);

            return obj;
        }

        public static void ReturnToPool(CombatEntityId obj, string enemyType) 
            => _instance.ReturnToPoolImpl(obj, enemyType);

        private void ReturnToPoolImpl(CombatEntityId obj, string enemyType) 
            => _pool[enemyType].Enqueue(obj);
    }
}