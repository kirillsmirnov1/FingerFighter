using FingerFighter.Utils;
using FingerFighter.View.Display;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils;

namespace FingerFighter.View.Ring
{
    [RequireComponent(typeof(Button))]
    public class LevelPickerButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelName;
        [SerializeField] private TextMeshProUGUI levelCost;
        [SerializeField] private Button button;

        [Header("Data")]
        [SerializeField] private StringSetVariable boughtLevels; // TODO subscribe on changes 
        
        private LevelPicker _levelPicker;
        private int _index;
        private string _packId;

        private void OnValidate()
        {
            button ??= GetComponent<Button>();
            this.CheckNullFields();
        }

        public void Init(LevelPicker levelPicker, int index, string packId)
        {
            _levelPicker = levelPicker;
            _index = index;
            _packId = packId;

            SetVisuals();
            
            button.onClick.AddListener(OnClick);
        }

        private void SetVisuals()
        {
            SetText();
            SetCostVisibility();
        }

        private void SetText()
        {
            levelName.text = _packId;
            // TODO init cost
        }

        private void SetCostVisibility()
        {
            levelCost.gameObject.SetActive(!LevelBought);
        }

        private void OnClick()
        {
            if (LevelBought)
            {
                _levelPicker.LoadLevel(_packId);
            }
            else
            {
                UiNotifications.Show("TODO buying attempt");
            }
        }

        private bool LevelBought => boughtLevels.Has(_packId);
    }
}