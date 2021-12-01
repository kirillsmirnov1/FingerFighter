using UnityEditor;
using UnityEngine;

namespace FingerFighter.View.LevelMaps
{
    [CustomEditor(typeof(LevelMapDisplay))]
    public class LevelMapsDisplayInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Spawn Map"))
            {
                (target as LevelMapDisplay).SpawnMap();
            }
            if (GUILayout.Button("Clear Map"))
            {
                (target as LevelMapDisplay).ClearMap();
            }
        }
    }
}