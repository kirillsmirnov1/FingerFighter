using System;
using UnityEngine;

namespace FingerFighter.Model.Combat
{
    [CreateAssetMenu(menuName = "Data/EnemyPrefabs", fileName = "EnemyPrefabs", order = 0)]
    public class EnemyPrefabs : ScriptableObject
    {
        [SerializeField] private EnemyAndTag[] enemies;  
        
        private void OnValidate() => InitEnemyTags();

        private void InitEnemyTags()
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].tag = enemies[i].prefab.GetComponent<CombatEntityId>().EnemyType;
            }
        }

        public int Length => enemies.Length;

        public EnemyAndTag this[int i] => enemies[i];

        [Serializable]
        public struct EnemyAndTag
        {
            public GameObject prefab;
            public string tag;
        }
    }
}