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
        [SerializeField] private GameObject checkMarkOverMarker; // TODO enable for passed locations 
        [SerializeField] private Image markersImage;

        [Header("Start")]
        [SerializeField] private Sprite startSprite;
        [SerializeField] private Color startColor = Color.white;

        [Header("Boss")]
        [SerializeField] private Sprite crownSprite;
        [SerializeField] private Color crownColor = Color.yellow;
        
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
            SetVisuals();
        }

        private void SetVisuals()
        {
            switch (_data.type)
            {
                case RoomType.Start:
                    markersImage.sprite = startSprite;
                    markersImage.color = startColor;
                    break;
                case RoomType.Regular:
                    // TODO set checkmark 
                    // TODO set color 
                    break;
                case RoomType.Boss:
                    markersImage.sprite = crownSprite;
                    markersImage.color = crownColor;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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