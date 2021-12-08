using FingerFighter.Control.LevelMaps;
using FingerFighter.Control.Scenes;
using FingerFighter.Model.EnemyFormations;
using UnityEngine;
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
                Instantiate(levelPickerButtonPrefab, transform)
                    .GetComponent<LevelPickerButton>()
                    .Init(this, i, pack.Id, pack.cost);
            }
        }

        public void LoadLevel(string packId)
        {
            currentRoom.Value = 0;
            levelNameVar.Value = packId;
            levelMapGenerator.Generate();
            SceneManagerCustom.LoadScene(levelMapScene.sceneName);
        }
    }
}
