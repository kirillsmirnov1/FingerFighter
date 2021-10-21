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

        private void MoveEntries()
        {
            var formation = target as EnemyFormationEditor;
            // ReSharper disable once PossibleNullReferenceException
            var transform = formation.transform;
            for (int i = 0; i < formation.formationEntries.Length; i++)
            {
                var oldPoint = transform.TransformPoint(formation.formationEntries[i].pos);
                var newPoint = Handles.FreeMoveHandle(oldPoint, Quaternion.identity, HandleSize, Snap, Handles.DotHandleCap);
                if (oldPoint != newPoint)
                {
                    Undo.RecordObject(formation, "Move");
                    formation.formationEntries[i].pos = transform.InverseTransformPoint(newPoint);
                }
            }
        }
    }
}