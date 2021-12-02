using System;
using FingerFighter.Model.LevelMaps;
using UnityEngine;

namespace FingerFighter.View.LevelMaps.Player
{
    public class PlayerMarker : MonoBehaviour
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

        private abstract class State
        {
            protected readonly PlayerMarker Marker;
            protected readonly Transform Self;
            protected State(PlayerMarker marker)
            {
                Marker = marker;
                Self = Marker.transform;
            }

            public abstract void OnRoomMarkerClicked(int roomIndex);

            public abstract void OnUpdate();
        }

        private class AtPosition : State
        {
            public AtPosition(PlayerMarker marker, int roomIndex) : base(marker)
            {
                Self.position = Marker.RoomMarkerScreenPosition(roomIndex);
            }
            
            public override void OnRoomMarkerClicked(int roomIndex)
            {
                Marker._state = new Moves(Marker, roomIndex);
            }

            public override void OnUpdate()
            {
                // Does nothing
            }
        }

        private class Moves : State
        {
            private readonly Vector3 _targetPos;
            private readonly int _targetRoom;
            
            public Moves(PlayerMarker marker, int roomIndex) : base(marker)
            {
                _targetRoom = roomIndex;
                _targetPos = Marker.RoomMarkerScreenPosition(roomIndex);
            }
            
            public override void OnRoomMarkerClicked(int roomIndex)
            {
                // Does nothing 
            }

            public override void OnUpdate()
            {
                MoveToTarget();
            }
            
            private void MoveToTarget()
            {
                if (Vector3.Distance(Self.position, _targetPos) > Marker.eps)
                {
                    Self.position += (_targetPos - Self.position).normalized * (Time.deltaTime * Marker.speed);
                }
                else
                {
                    RoomReached();
                }
            }
            
            private void RoomReached()
            {
                OnRoomReached?.Invoke(_targetRoom);
                Marker._state = new AtPosition(Marker, _targetRoom);
            }
        }
    }
}