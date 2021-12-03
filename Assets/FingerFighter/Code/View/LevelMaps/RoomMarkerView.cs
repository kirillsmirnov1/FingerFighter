using System;
using System.Collections.Generic;
using FingerFighter.Model.LevelMaps;
using FingerFighter.View.LevelMaps.Player;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils;
using UnityUtils.Variables;

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
        [SerializeField] private IntVariable currentPlayerRoom;
        [SerializeField] private RoomsStatus roomsStatus;
        
        [Header("Start")]
        [SerializeField] private Sprite startSprite;
        [SerializeField] private Color startColor = Color.white;

        [Header("Regular")]
        [SerializeField] private Gradient regularRoomColors;
        [SerializeField] private Color usedFade = Color.gray;
        
        [Header("Boss")]
        [SerializeField] private Sprite crownSprite;
        [SerializeField] private Color crownColor = Color.yellow;
        
        private RoomMarkerData _data;
        private int Index => _data.roomIndex;
        private RoomType RoomType => _data.type;
        private RoomStatus Status => roomsStatus[Index];
        private HashSet<int> Neighbours => _data.neighbours;

        private void OnValidate()
        {
            rect ??= GetComponent<RectTransform>();
            button ??= GetComponent<Button>();
            this.CheckNullFields();
        }

        private void Awake()
        {
            button.onClick.AddListener(NotifyOnClick);
            OnClick += OnRoomMarkerClicked;
            PlayerMarker.OnRoomReached += OnPlayerReachedRoom;
            roomsStatus.OnEntryChange += SetRoomStatus;
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(NotifyOnClick);
            OnClick -= OnRoomMarkerClicked;
            PlayerMarker.OnRoomReached -= OnPlayerReachedRoom;
            roomsStatus.OnEntryChange -= SetRoomStatus;
        }

        private void Start()
        {
            OnPlayerReachedRoom(currentPlayerRoom);
            SetRoomStatus(Index, Status);
        }

        private void SetRoomStatus(int roomIndex, RoomStatus newStatus)
        {
            if(roomIndex != Index) return;
            checkMarkOverMarker.gameObject.SetActive(newStatus == RoomStatus.Used && RoomType != RoomType.Start);
            SetColor();
        }

        private void OnRoomMarkerClicked(int roomIndex)
        {
            button.interactable = false;
        }

        private void NotifyOnClick()
        {
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
            SetSprite();
            SetColor();
        }
        
        private void SetSprite()
        {
            switch (_data.type)
            {
                case RoomType.Start:
                    markersImage.sprite = startSprite;
                    break;
                case RoomType.Regular:
                    break;
                case RoomType.Boss:
                    markersImage.sprite = crownSprite;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void SetColor()
        {
            var used = Status == RoomStatus.Used;
            switch (_data.type)
            {
                case RoomType.Start:
                    markersImage.color = startColor;
                    break;
                case RoomType.Regular:
                    markersImage.color = regularRoomColors.Evaluate(_data.difficulty);
                    if (used) markersImage.color -= usedFade;
                    break;
                case RoomType.Boss:
                    markersImage.color = crownColor;
                    if (used) markersImage.color -= usedFade;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnPlayerReachedRoom(int roomIndex)
        {
            if (roomIndex == Index)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = Neighbours.Contains(roomIndex);
            }
        }
    }
}