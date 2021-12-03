using UnityEditor;
using UnityEngine;

namespace FingerFighter.Control.LevelMaps
{
    [CustomEditor(typeof(LevelMapGenerator))]
    public class RoomsGraphGeneratorInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Generate"))
            {
                (target as LevelMapGenerator).Generate();
            }
        }
    }
}