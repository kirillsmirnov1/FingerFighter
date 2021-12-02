using System;
using FingerFighter.Model.LevelMaps;
using FingerFighter.View.LevelMaps.Player;
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
        private int Index => _data.roomIndex;
        
        private void OnValidate()
        {
            rect ??= GetComponent<RectTransform>();
            button ??= GetComponent<Button>();
        }

        private void Awake()
        {
            button.onClick.AddListener(NotifyOnClick);
            PlayerMarker.OnRoomReached += OnPlayerReachedRoom;
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(NotifyOnClick);
            PlayerMarker.OnRoomReached -= OnPlayerReachedRoom;
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

        private void OnPlayerReachedRoom(int roomIndex)
        {
            if (roomIndex == Index)
            {
                Debug.Log($"Yay! Player reached room {roomIndex}");
                // TODO disable button 
            }
            else
            {
                // todo check neighbours to enable / disable button 
            }
        }
    }
}