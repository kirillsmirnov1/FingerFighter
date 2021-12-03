using System;
using FingerFighter.Model.LevelMaps;
using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.View.LevelMaps.Player
{
    public partial class PlayerMarker : MonoBehaviour
    {
        public static event Action<int> OnRoomReached; 

        [SerializeField] private float speed = 1;
        [SerializeField] private float eps = 0.1f;
        [SerializeField] private LevelMapVariable levelMapVariable;
        [SerializeField] private IntVariable currentRoom;
        
        private State _state;

        private void Awake() 
            => RoomMarkerView.OnClick += OnRoomMarkerClicked;
        private void OnDestroy() 
            => RoomMarkerView.OnClick -= OnRoomMarkerClicked;

        private void Start()
            => SetPosition(currentRoom);   

        private void Update() 
            => _state.OnUpdate();

        public void SetPosition(int roomIndex)
        {
            _state = new AtPosition(this, roomIndex);
        }
        
        private void OnRoomMarkerClicked(int roomIndex) 
            => _state.OnRoomMarkerClicked(roomIndex);

        private Vector2 RoomMarkerScreenPosition(int roomIndex) 
            => Camera.main.WorldToScreenPoint(levelMapVariable.Value.rooms[roomIndex].pos);
    }
}