using System;
using FingerFighter.Model.LevelMaps;
using UnityEngine;

namespace FingerFighter.View.LevelMaps.Player
{
    public partial class PlayerMarker : MonoBehaviour
    {
        public static event Action<int> OnRoomReached; 

        [SerializeField] private LevelMapVariable levelMapVariable;
        [SerializeField] private float speed = 1;
        [SerializeField] private float eps = 0.1f;

        private State _state;

        private void Awake() 
            => RoomMarkerView.OnClick += OnRoomMarkerClicked;
        private void OnDestroy() 
            => RoomMarkerView.OnClick -= OnRoomMarkerClicked;

        private void Start() 
            => _state = new AtPosition(this, 0); // TODO position on current room   

        private void Update() 
            => _state.OnUpdate();

        private void OnRoomMarkerClicked(int roomIndex) 
            => _state.OnRoomMarkerClicked(roomIndex);

        private Vector2 RoomMarkerScreenPosition(int roomIndex) 
            => Camera.main.WorldToScreenPoint(levelMapVariable.Value.rooms[roomIndex].pos);
    }
}