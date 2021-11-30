using UnityEditor;
using UnityEngine;

namespace FingerFighter.Control.LevelMap
{
    [CustomEditor(typeof(RoomsGraphGenerator))]
    public class RoomsGraphGeneratorInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Generate"))
            {
                (target as RoomsGraphGenerator).Generate();
            }
        }
    }
}