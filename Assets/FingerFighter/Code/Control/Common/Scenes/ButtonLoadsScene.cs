using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils.Scenes;

namespace FingerFighter.Control.Common.Scenes
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

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
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

        private void OnClick()
        {
            SceneManagerCustom.LoadScene(sceneNameReference.sceneName);
        }
    }
}