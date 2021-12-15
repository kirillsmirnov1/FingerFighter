using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityUtils.SO;

namespace FingerFighter.Model.Common.Combat
{
    [CreateAssetMenu(menuName = "Data/EnemyStats", fileName = "EnemyStats", order = 0)]
    public class EnemyStatsList : InitiatedScriptableObject
    {
        [SerializeField] private List<EnemyStats> stats;
        
        private readonly Dictionary<string, EnemyStats> _statsDictionary = new Dictionary<string, EnemyStats>();

        public int Count => stats.Count;
        public EnemyStats this[int i] => stats[i];
        public EnemyStats this[string tag] => _statsDictionary[tag];

        public override void Init() => FillStatsDictionary();

        private void FillStatsDictionary()
        {
            for (var i = 0; i < Count; i++)
            {
                _statsDictionary.Add(stats[i].tag, stats[i]);
            }
        }

#if UNITY_EDITOR
        public void Add()
        {
            var newOne = CreateInstance<EnemyStats>();
            AssetDatabase.AddObjectToAsset(newOne, this);
            stats.Add(newOne);
        }

        public void Remove()
        {
            var lastOne = stats[stats.Count - 1];
            stats.Remove(lastOne);
            AssetDatabase.RemoveObjectFromAsset(lastOne);
        }
#endif
    }
}