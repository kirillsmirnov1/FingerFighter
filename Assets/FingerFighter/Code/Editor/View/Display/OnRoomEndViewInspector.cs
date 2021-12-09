using UnityEditor;
using UnityEngine;

namespace FingerFighter.View.Display
{
    [CustomEditor(typeof(OnRoomEndView))]
    public class OnRoomEndViewInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            var view = target as OnRoomEndView;
            if(GUILayout.Button("Preview Death")) view.ShowOnDeath();
            if(GUILayout.Button("Preview Win")) view.ShowOnWin();
            base.OnInspectorGUI();
        }
    }
}