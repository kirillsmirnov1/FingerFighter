using System;
using UnityEditor;
using UnityEngine;

namespace FingerFighter.Model.Combat
{
    [CreateAssetMenu(menuName = "Data/EnemyStats", fileName = "EnemyStats", order = 0)]
    public class EnemyStatsData : ScriptableObject
    {
        [SerializeField] private EnemyStats[] stats;

        // TODO On creation provide empty instance 
        // TODO provide way to remove instances 
        private void OnValidate()
        {
            FillNullInstances();
        }

        private void FillNullInstances()
        {
#if UNITY_EDITOR
            for (int i = 0; i < stats.Length; i++)
            {
                if (stats[i] == null)
                {
                    stats[i] = CreateInstance<EnemyStats>();
                    AssetDatabase.AddObjectToAsset(stats[i], this);
                }
            }
#endif
        }

        public EnemyStats GetData(string tag)
        {
            for (int i = 0; i < stats.Length; i++)
            {
                if (stats[i].tag.Equals(tag)) return stats[i];
            }
            throw new ArgumentOutOfRangeException();
        }
        
        public bool HasEnemy(string tag)
        {
            for (int i = 0; i < stats.Length; i++)
            {
                if (stats[i].tag.Equals(tag)) return true;
            }
            return false;
        }
    }
}