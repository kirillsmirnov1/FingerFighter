using FingerFighter.Utils;
using FingerFighter.View.Common;
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
        [SerializeField] private StringSetVariable boughtLevels; 
        
        private LevelPicker _levelPicker;
        private int _index;
        private string _packId;
        private ulong _packCost;

        private void OnValidate()
        {
            button ??= GetComponent<Button>();
            this.CheckNullFields();
        }

        private void Awake()
        {
            boughtLevels.OnChangeBase += SetCostVisibility;
        }

        private void OnDestroy()
        {
            boughtLevels.OnChangeBase -= SetCostVisibility;
        }

        public void Init(LevelPicker levelPicker, int index, string packId, ulong packCost)
        {
            _levelPicker = levelPicker;
            _index = index;
            _packId = packId;
            _packCost = packCost;

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
            levelCost.text = $"<sprite index=0 tint=1> {_packCost}";
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
                if (_levelPicker.CanBuy(_packCost))
                {
                    _levelPicker.Buy(_packId, _packCost);
                }
                else
                {
                    UiNotifications.Show("Don't have enough coins");
                }
            }
        }

        private bool LevelBought => boughtLevels.Has(_packId);
    }
}