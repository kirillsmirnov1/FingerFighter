using FingerFighter.Control.LevelMaps;
using FingerFighter.Model.EnemyFormations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityUtils;
using UnityUtils.Scenes;
using UnityUtils.Variables;

namespace FingerFighter.View.Ring
{
    public class LevelPicker : MonoBehaviour
    {
        [SerializeField] private GameObject levelPickerButtonPrefab;
        [SerializeField] private EnemyFormationPackArray packs;
        [SerializeField] private SceneNameReference levelMapScene;
        [SerializeField] private StringVariable levelNameVar;
        [SerializeField] private LevelMapGenerator levelMapGenerator;
        [SerializeField] private IntVariable currentRoom;
        
        private void OnValidate()
        {
            this.CheckNullFields();
#if UNITY_EDITOR
            levelMapScene.SerializeName();
#endif
        }

        private void Awake()
        {
            SpawnButtons();
        }

        private void SpawnButtons()
        {
            for (var i = 0; i < packs.Value.Length; i++)
            {
                var pack = packs.Value[i];
                var buttonGO = Instantiate(levelPickerButtonPrefab, transform);
                // TODO use LevelPickerButton
                buttonGO.GetComponentInChildren<TextMeshProUGUI>().text = pack.Id;
                var capturedIndex = i;
                buttonGO.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(capturedIndex));
            }
        }

        private void OnButtonClick(int index)
        {
            var pack = packs[index];
            currentRoom.Value = 0;
            levelNameVar.Value = pack.Id;
            levelMapGenerator.Generate();
            SceneManager.LoadScene(levelMapScene.sceneName);
        }
    }
}
