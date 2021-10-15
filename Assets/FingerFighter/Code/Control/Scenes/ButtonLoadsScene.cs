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
        [SerializeField] private SceneAsset sceneAsset;
        [SerializeField] private TextMeshProUGUI sceneName;

        private void OnValidate()
        {
            if (sceneAsset != null) SetSceneNameText();
        }

        public void Init(SceneAsset scene)
        {
            sceneAsset = scene;
            SetSceneNameText();
        }

        private void SetSceneNameText()
        {
            if (sceneName != null)
                sceneName.text = sceneAsset.name;
        }

        public void OnClick()
        {
            Debug.Log($"Clicked on {sceneAsset.name} button");
            SceneManager.LoadScene(sceneAsset.name);
        }
    }
}