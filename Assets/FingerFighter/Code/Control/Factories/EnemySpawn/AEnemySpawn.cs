﻿using System.Collections.Generic;
using FingerFighter.Model.Combat;
using FingerFighter.View;
using UnityEngine;

namespace FingerFighter.Control.Factories.EnemySpawn
{
    public abstract class AEnemySpawn : JumpObjectOnOutOfCamera
    {
        [SerializeField] protected EnemyStatsList enemyData;
        
        private readonly Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
        private readonly Dictionary<string, Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();
        
        private Transform anchor;
        private static AEnemySpawn _instance;
        private Vector2 _currentPos;
        private int _aliveEnemies;

        protected virtual void Awake()
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
            var instance = GetFromPool(enemyTag);
            instance.SetActive(true);
            instance.transform.position = pos;
            UpdateAliveEnemyCount(1);
        }

        protected GameObject GetFromPool(string enemyTag)
        {
            return pool[enemyTag].Count > 0 
                ? pool[enemyTag].Dequeue() 
                : Instantiate(prefabs[enemyTag], anchor);
        }

        protected virtual void ReturnToPoolImpl(GameObject obj, string enemyType)
        {
            pool[enemyType].Enqueue(obj);
            UpdateAliveEnemyCount(-1);
        }

        private void UpdateAliveEnemyCount(int change)
        {
            _aliveEnemies += change;
            if (_aliveEnemies == 0)
            {
                NoEnemiesLeftAlive();
            }
        }

        protected virtual void NoEnemiesLeftAlive()
        {
            Debug.Log("No enemies left alive");
        }

        public static void ReturnToPool(GameObject obj, string enemyType)
        {
            _instance?.ReturnToPoolImpl(obj, enemyType);
        }
    }
}