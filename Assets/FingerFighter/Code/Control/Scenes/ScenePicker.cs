using UnityEditor;
using UnityEngine;

namespace FingerFighter.Control.Scenes
{
    public class ScenePicker : MonoBehaviour
    {
        [SerializeField] private SceneAsset[] scenes;
        [SerializeField] private GameObject buttonPrefab;

        private void Awake()
        {
            SpawnSceneButtons();
        }

        private void SpawnSceneButtons()
        {
            for (int i = 0; i < scenes.Length; i++)
            {
                Instantiate(buttonPrefab, transform)
                    .GetComponent<ButtonLoadsScene>()
                    .Init(scenes[i]);
            }
        }
    }
}