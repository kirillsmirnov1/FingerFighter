using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FingerFighter.Model.Combat
{
    [CreateAssetMenu(menuName = "Data/EnemyStats", fileName = "EnemyStats", order = 0)]
    public class EnemyStatsList : ScriptableObject
    {
        [SerializeField] private List<EnemyStats> stats;
        
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
    
#if UNITY_EDITOR
    [CustomEditor(typeof(EnemyStatsList))]
    public class EnemyStatsListEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var statsList = target as EnemyStatsList;
            if(statsList == null) return;
            
            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Add"))
                {
                    statsList.Add();
                }

                if (GUILayout.Button("Remove"))
                {
                    statsList.Remove();
                }
            }
            GUILayout.EndHorizontal();

            base.OnInspectorGUI();
        }
    }
#endif
}