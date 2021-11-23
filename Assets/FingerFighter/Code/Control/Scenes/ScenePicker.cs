using UnityEngine;
using UnityUtils.Scenes;

namespace FingerFighter.Control.Scenes
{
    public class ScenePicker : MonoBehaviour
    {
        [SerializeField] private SceneNameReference[] sceneNameReference;
        [SerializeField] private GameObject buttonPrefab;

        private void OnValidate()
        {
            #if UNITY_EDITOR
                for (var i = 0; i < sceneNameReference.Length; i++)
                {
                    sceneNameReference[i].SerializeName();
                }
            #endif
        }

        private void Awake()
        {
            SpawnSceneButtons();
        }

        private void SpawnSceneButtons()
        {
            for (int i = 0; i < sceneNameReference.Length; i++)
            {
                Instantiate(buttonPrefab, transform)
                    .GetComponent<ButtonLoadsScene>()
                    .Init(sceneNameReference[i]);
            }
        }
    }
}