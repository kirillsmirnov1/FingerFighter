using UnityEditor;
using UnityEngine;

namespace FingerFighter.Model.EnemyFormations
{
    [CustomPropertyDrawer(typeof(EnemyFormationEntry))]
    public class EnemyFormationEntryDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, GUIContent.none, property);
            {
                var totalWidth = position.width;
                // Type
                position.width = 0.25f * totalWidth;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("enemy"), GUIContent.none);
                
                // Gap
                position.width += 0.01f * totalWidth;
                
                // Pos
                position.x += position.width;
                position.width = 0.74f * totalWidth;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("pos"), GUIContent.none);
            }
            EditorGUI.EndProperty();
        }
    }
}