using System;
using System.Collections;
using FingerFighter.View.Display.SceneChangeHex;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityUtils;

namespace FingerFighter.Control.Scenes
{
    public class SceneManagerCustom : MonoBehaviour
    {
        [SerializeField] private HexGrid hexes;
        [SerializeField] private float duration = 1f;
        [SerializeField] private float stepDuration = 0.05f;
        
        private static SceneManagerCustom _instance;

        private void OnValidate()
        {
            this.CheckNullFields();
        }

        private void Awake()
        {
            _instance = this;
        }

        public static void LoadScene(string sceneName) 
            => _instance.LoadSceneImpl(sceneName);

        private void LoadSceneImpl(string sceneName)
        {
            StartCoroutine(SceneLoadCoroutine(sceneName));
        }

        private IEnumerator SceneLoadCoroutine(string sceneName)
        {
            var wfs = new WaitForSeconds(stepDuration);
            hexes.gameObject.SetActive(true);
            if (Math.Abs(hexes.t - 1f) > 0.001f)
            {
                hexes.Scale(0);

                for (float i = 0; i <= duration; i += stepDuration)
                {
                    hexes.Scale(i / duration);
                    yield return wfs;
                }
            }

            SceneManager.LoadScene(sceneName);
            
            for (float i = duration; i >= 0; i -= stepDuration)
            {
                hexes.Scale(i/duration);
                yield return wfs;
            }
            
            hexes.Scale(0);
            hexes.gameObject.SetActive(false);
        }
    }
}
