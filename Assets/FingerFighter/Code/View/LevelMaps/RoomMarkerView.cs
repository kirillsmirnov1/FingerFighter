using System;
using FingerFighter.Model.LevelMaps;
using UnityEngine;
using UnityEngine.UI;

namespace FingerFighter.View.LevelMaps
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(RectTransform))]
    public class RoomMarkerView : MonoBehaviour
    {
        public static event Action<int> OnClick; 

        [SerializeField] private RectTransform rect;
        [SerializeField] private Button button;

        private RoomMarkerData _data;
        
        // TODO set button interaction depending on proximity to player

        private void OnValidate()
        {
            rect ??= GetComponent<RectTransform>();
            button ??= GetComponent<Button>();
        }

        private void Awake()
        {
            button.onClick.AddListener(NotifyOnClick);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(NotifyOnClick);
        }

        private void NotifyOnClick()
        {
            OnClick?.Invoke(_data.roomIndex);
        }

        public void Init(RoomMarkerData data)
        {
            _data = data;
            rect.position = _data.position;
        }
    }
}