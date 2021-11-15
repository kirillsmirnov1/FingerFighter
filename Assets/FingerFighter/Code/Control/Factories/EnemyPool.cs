using System.Collections.Generic;
using FingerFighter.Model.Combat;
using UnityEngine;

namespace FingerFighter.Control.Factories
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] private EnemyStatsList enemyData;
        
        private readonly Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();
        private readonly Dictionary<string, Queue<GameObject>> _pool = new Dictionary<string, Queue<GameObject>>();
        
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
                _pool.Add(enemyData[i].tag, new Queue<GameObject>());
                _prefabs.Add(enemyData[i].tag, enemyData[i].prefab);
            }
        }

        public static GameObject Get(string enemyTag) => _instance.GetImpl(enemyTag);

        private GameObject GetImpl(string enemyTag)
        {
            return _pool[enemyTag].Count > 0 
                ? _pool[enemyTag].Dequeue() 
                : Instantiate(_prefabs[enemyTag], _anchor);
        }

        public static void ReturnToPool(GameObject obj, string enemyType) 
            => _instance.ReturnToPoolImpl(obj, enemyType);

        private void ReturnToPoolImpl(GameObject obj, string enemyType) 
            => _pool[enemyType].Enqueue(obj);
    }
}