using UnityEngine;
using UnityEngine.SceneManagement;
using UnityUtils.Scenes;

namespace FingerFighter.Audio
{
    public class GameMusic : MonoBehaviour
    {
        [SerializeField] private AudioSource ambientMusic;
        [SerializeField] private AudioSource actionMusic;
        [SerializeField] private SceneNameReference runnerScene;
        
        private bool _action;

        private void OnValidate()
        {
#if UNITY_EDITOR
            runnerScene.SerializeName();
#endif
        }

        private void Awake() 
            => SceneManager.sceneLoaded += OnSceneLoaded;

        private void OnDestroy() 
            => SceneManager.sceneLoaded -= OnSceneLoaded;

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            var newAction = scene.name.Equals(runnerScene.sceneName);
            SetAction(newAction);
        }

        private void SetAction(bool newAction)
        {
            if(newAction == _action) return;
            _action = newAction;
            if (_action)
            {
                actionMusic.Play();
                ambientMusic.Pause();
            }
            else
            {
                actionMusic.Pause();
                ambientMusic.Play();
            }
        }
    }
}
