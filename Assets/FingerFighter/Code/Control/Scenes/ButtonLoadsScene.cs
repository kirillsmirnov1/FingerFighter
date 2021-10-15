﻿using FingerFighter.Model.Scenes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FingerFighter.Control.Scenes
{
    [RequireComponent(typeof(Button))]
    public class ButtonLoadsScene : MonoBehaviour
    {
        [SerializeField] private SceneNameReference sceneNameReference;
        [SerializeField] private TextMeshProUGUI sceneName;

        private void OnValidate()
        {
            #if UNITY_EDITOR
                sceneNameReference.SerializeName();
            #endif
            if (sceneNameReference.sceneName != null) SetSceneNameText();
        }

        public void Init(SceneNameReference sceneNameRef)
        {
            sceneNameReference = sceneNameRef;
            SetSceneNameText();
        }

        private void SetSceneNameText()
        {
            if (sceneName != null)
                sceneName.text = sceneNameReference.sceneName;
        }

        public void OnClick()
        {
            Debug.Log($"Clicked on {sceneNameReference.sceneName} button");
            SceneManager.LoadScene(sceneNameReference.sceneName);
        }
    }
}