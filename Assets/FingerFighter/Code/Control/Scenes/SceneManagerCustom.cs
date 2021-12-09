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
        [SerializeField] private float splashDuration = 0.5f;

        [Header("Fade")]
        [SerializeField] private float t = 0;
        
        private static SceneManagerCustom _instance;
        private WaitForSeconds _wfsStep;

        private void OnValidate()
        {
            this.CheckNullFields();
        }

        private void Awake()
        {
            _wfsStep = new WaitForSeconds(stepDuration);
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
            yield return FadeIn();
            yield return DelayedSceneLoad(loadSceneAction);
            yield return FadeOut();
        }

        private IEnumerator FadeIn()
        {
            hexes.Scale(t);
            hexes.gameObject.SetActive(true);

            for (float i = t * duration; i <= duration; i += stepDuration)
            {
                hexes.Scale(t = i / duration);
                yield return _wfsStep;
            }
        }

        private IEnumerator DelayedSceneLoad(Action loadSceneAction)
        {
            yield return new WaitForSeconds(splashDuration);
            loadSceneAction.Invoke();
        }

        private IEnumerator FadeOut()
        {
            for (float i = duration; i >= 0; i -= stepDuration)
            {
                hexes.Scale(t = i/duration);
                yield return _wfsStep;
            }
            
            hexes.Scale(t = 0);
            hexes.gameObject.SetActive(false);
        }

        public static void Reload() 
            => LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
