using FingerFighter.Utils;
using UnityEditor;
using UnityEngine;

namespace FingerFighter.Model.EnemyFormations
{
    [CustomEditor(typeof(EnemyFormationEditor))]
    public class EnemyFormationEditorInspector : Editor
    {
        private const float HandleSize = 0.06f;
        private static readonly Vector3 Snap = new Vector3(0.1f, 0.1f, 0.1f);

        private void OnSceneGUI()
        {
            MoveEntries();
        }

        public override void OnInspectorGUI()
        {
            DisplayData();
        }

        private void MoveEntries()
        {
            var formation = target as EnemyFormationEditor;
            // ReSharper disable once PossibleNullReferenceException
            var transform = formation.transform;
            if(formation.formation.entries == null) return;
            for (int i = 0; i < formation.formation.entries.Length; i++)
            {
                var entry = formation.formation.entries[i];
                
                var handleColor = Color.white - entry.enemy.gizmoColor;
                handleColor.a = 1;
                Handles.color = handleColor;
                
                var oldPoint = transform.TransformPoint(entry.pos);
                var newPoint = Handles.FreeMoveHandle(oldPoint, Quaternion.identity, HandleSize, Snap, Handles.DotHandleCap);

                if (oldPoint != newPoint)
                {
                    Undo.RecordObject(formation, "Move");
                    formation.formation.entries[i].pos = transform.InverseTransformPoint(newPoint);
                    formation.UpdatePack();
                }
            }
        }

        private void DisplayData()
        {
            var packEditor = serializedObject.FindProperty("packEditor");
            var id = serializedObject.FindProperty("formation.id");
            var formation = serializedObject.FindProperty("formation.entries");
            
            serializedObject.Update();
            {
                EditorGUILayout.PropertyField(packEditor);
                EditorGUILayout.LabelField(new GUIContent("Formation"));
                EditorGUILayout.PropertyField(id);
                EditorList.Show(formation, EditorListOption.ListSize | EditorListOption.Buttons);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}