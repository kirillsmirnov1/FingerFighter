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
            => _instance.LoadSceneImpl(() => SceneManager.LoadScene(sceneName));
        public static void LoadScene(int sceneBuildIndex)
            => _instance.LoadSceneImpl(() => SceneManager.LoadScene(sceneBuildIndex));
        
        private void LoadSceneImpl(Action loadSceneAction)
        {
            StopAllCoroutines();
            StartCoroutine(SceneLoadCoroutine(loadSceneAction));
        }

        private IEnumerator SceneLoadCoroutine(Action loadSceneAction)
        {
            var wfs = new WaitForSeconds(stepDuration);
            
            hexes.Scale(0);
            hexes.gameObject.SetActive(true);

            for (float i = 0; i <= duration; i += stepDuration)
            {
                hexes.Scale(i / duration);
                yield return wfs;
            }
            
            loadSceneAction.Invoke();
            
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
