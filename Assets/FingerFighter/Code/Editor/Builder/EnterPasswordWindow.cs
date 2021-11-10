using System;
using UnityEditor;
using UnityEngine;

namespace FingerFighter.Builder
{
    public class EnterPasswordWindow : EditorWindow
    {
        private string _pass;
        private static Action<string> _callback;
        
        public static void ShowWindow(Action<string> passCallback = null)
        {
            _callback = passCallback;
            GetWindow<EnterPasswordWindow>("Enter password");
        }
        
        private void OnGUI()
        {
            _pass = EditorGUILayout.TextField("Password", _pass);
            if (GUILayout.Button("Ok"))
            {
                Close();
                _callback?.Invoke(_pass);
            }
        }
    }
}