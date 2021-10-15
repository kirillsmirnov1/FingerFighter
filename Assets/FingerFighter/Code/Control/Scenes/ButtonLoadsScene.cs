using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FingerFighter.Control.Scenes
{
    [RequireComponent(typeof(Button))]
    public class ButtonLoadsScene : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI sceneName;

        private SceneAsset _sceneAsset;
        
        public void Init(SceneAsset scene)
        {
            _sceneAsset = scene;
            sceneName.text = scene.name;
        }

        public void OnClick()
        {
            Debug.Log($"Clicked on {_sceneAsset.name} button");
            SceneManager.LoadScene(_sceneAsset.name);
        }
    }
}