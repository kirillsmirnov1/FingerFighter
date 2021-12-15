using FingerFighter.Model.Common.Combat;
using UnityEditor;
using UnityEngine;

namespace FingerFighter.Model.Combat
{
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
}