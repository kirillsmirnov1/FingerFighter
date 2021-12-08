using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils;

namespace FingerFighter.View.Ring
{
    [RequireComponent(typeof(Button))]
    public class LevelPickerButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Button button;
        
        private LevelPicker _levelPicker;
        private int _index;
        private string _packId;

        private void OnValidate()
        {
            text ??= GetComponentInChildren<TextMeshProUGUI>();
            button ??= GetComponent<Button>();
            this.CheckNullFields();
        }

        public void Init(LevelPicker levelPicker, int index, string packId)
        {
            _levelPicker = levelPicker;
            _index = index;
            _packId = packId;
            text.text = _packId;
            button.onClick.AddListener(() => _levelPicker.OnButtonClick(_index));
        }
    }
}