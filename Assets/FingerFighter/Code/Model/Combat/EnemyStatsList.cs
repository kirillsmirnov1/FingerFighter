using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FingerFighter.Model.Combat
{
    [CreateAssetMenu(menuName = "Data/EnemyStats", fileName = "EnemyStats", order = 0)]
    public class EnemyStatsList : ScriptableObject
    {
        [SerializeField] private List<EnemyStats> stats;

        // TODO On creation provide empty instance 
        // TODO provide way to remove instances 
        private void OnValidate()
        {
            FillNullInstances();
        }

        private void FillNullInstances()
        {
#if UNITY_EDITOR
            for (int i = 0; i < stats.Count; i++)
            {
                if (stats[i] == null)
                {
                    stats[i] = CreateInstance<EnemyStats>();
                    AssetDatabase.AddObjectToAsset(stats[i], this);
                }
            }
#endif
        }
    }
}