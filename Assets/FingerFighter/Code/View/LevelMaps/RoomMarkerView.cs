using System;
using System.Collections.Generic;
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
        private HashSet<int> Neighbours => _data.neighbours;

        private void OnValidate()
        {
            rect ??= GetComponent<RectTransform>();
            button ??= GetComponent<Button>();
        }

        private void Awake()
        {
            button.onClick.AddListener(NotifyOnClick);
            OnClick += OnRoomMarkerClicked;
            PlayerMarker.OnRoomReached += OnPlayerReachedRoom;
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(NotifyOnClick);
            OnClick -= OnRoomMarkerClicked;
            PlayerMarker.OnRoomReached -= OnPlayerReachedRoom;
        }

        private void OnRoomMarkerClicked(int roomIndex)
        {
            button.interactable = false;
        }

        private void NotifyOnClick()
        {
            button.interactable = false;
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
            }
            else
            {
                button.interactable = Neighbours.Contains(roomIndex);
            }
        }
    }
}