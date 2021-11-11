using System;
using UnityEditor;
using UnityEngine;

namespace FingerFighter.Builder
{
    public class EnterPasswordWindow : EditorWindow
    {
        private string _pass;
        private static Action<string> _callback;

        public static void ShowWindow(Action<string> passCallback)
        {
            _callback = passCallback;
            var window = GetWindow<EnterPasswordWindow>("Enter password");
            var windowSize = new Vector2(400, 200);
            var windowPos = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height) / 2 - windowSize;
            window.position = new Rect(windowPos, windowSize);
            window.maxSize = windowSize;
        }
        
        private void OnGUI()
        {
            _pass = EditorGUILayout.PasswordField("Password", _pass);
            if (GUILayout.Button("Ok") || Event.current.keyCode == KeyCode.Return)
            {
                HandleInput();
            }
        }

        private void HandleInput()
        {
            Close();
            _callback?.Invoke(_pass);
        }
    }
}