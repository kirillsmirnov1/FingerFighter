using UnityEditor;
using UnityEngine;

namespace FingerFighter.Utils
{
    public static class EditorList
    {
        private static readonly GUIContent
            MoveButtonContent = new GUIContent("\u21b4", "move down"),
            DuplicateButtonContent = new GUIContent("+", "duplicate"),
            DeleteButtonContent = new GUIContent("-", "delete"),
            AddButtonContent = new GUIContent("+", "add element");

        private static readonly GUILayoutOption MiniButtonWidth = GUILayout.Width(20f);
        
        public static void Show(SerializedProperty list, EditorListOption options = EditorListOption.Default)
        {
            if (!list.isArray)
            {
                EditorGUILayout.HelpBox(list.name + " is not a list/array", MessageType.Error);
                return;
            }
            
            var showListLabel = (options & EditorListOption.ListLabel) != 0;
            var showListSize = (options & EditorListOption.ListSize) != 0;
            
            if (showListLabel)
            {
                list.isExpanded = EditorGUILayout.Foldout(list.isExpanded, list.displayName, true);
            }
            if (list.isExpanded || !showListLabel)
            {
                if(showListLabel) EditorGUI.indentLevel += 1;
                
                var size = list.FindPropertyRelative("Array.size");
                if(showListSize) EditorGUILayout.PropertyField(size);
                
                if (size.hasMultipleDifferentValues)
                {
                    EditorGUILayout.HelpBox("Not showing lists with different sizes", MessageType.Info);
                }
                else
                {
                    ShowArrayElements(list, options);
                }
                
                if(showListLabel) EditorGUI.indentLevel -= 1;
            }
        }
        private static void ShowArrayElements(SerializedProperty list, EditorListOption options)
        {
            var showElementLabels = (options & EditorListOption.ElementLabels) != 0;
            var showButtons = (options & EditorListOption.Buttons) != 0;
            
            for (int i = 0; i < list.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    var element = list.GetArrayElementAtIndex(i);
                    var label = showElementLabels ? new GUIContent(element.displayName) : GUIContent.none;
                    EditorGUILayout.PropertyField(element, label);
                    if (showButtons) ShowButtons(list, i);
                }
                EditorGUILayout.EndHorizontal();
            }

            if (showButtons && list.arraySize == 0)
            {
                if (GUILayout.Button(AddButtonContent, EditorStyles.miniButton))
                {
                    list.InsertArrayElementAtIndex(0);
                }
            }
        }

        private static void ShowButtons(SerializedProperty list, int i)
        {
            if (GUILayout.Button(MoveButtonContent, EditorStyles.miniButtonLeft, MiniButtonWidth))
            {
                list.MoveArrayElement(i, i + 1);
            }

            if (GUILayout.Button(DuplicateButtonContent, EditorStyles.miniButtonMid, MiniButtonWidth))
            {
                list.InsertArrayElementAtIndex(i);   
            }

            if (GUILayout.Button(DeleteButtonContent, EditorStyles.miniButtonRight, MiniButtonWidth))
            {
                int oldSize = list.arraySize;
                list.DeleteArrayElementAtIndex(i);
                if (list.arraySize == oldSize)
                {
                    list.DeleteArrayElementAtIndex(i);
                }
            }
        }
    }
}