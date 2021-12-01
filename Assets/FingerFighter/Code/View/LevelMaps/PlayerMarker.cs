using System;
using FingerFighter.Model.LevelMaps;
using UnityEngine;

namespace FingerFighter.View.LevelMaps
{
    public class PlayerMarker : MonoBehaviour
    {
        public static event Action<int> OnRoomReached; 

        [SerializeField] private LevelMapVariable levelMapVariable;
        [SerializeField] private float speed = 1;
        [SerializeField] private float eps = 0.1f;
        
        private Action _onUpdate;
        private Vector3 _targetPos;
        private int _targetRoom;

        private void Awake()
        {
            // TODO position on current room 
            RoomMarkerView.OnClick += OnRoomMarkerClicked;
        }

        private void OnDestroy()
        {
            RoomMarkerView.OnClick -= OnRoomMarkerClicked;
        }

        private void Update()
        {
            _onUpdate?.Invoke();
        }

        private void OnRoomMarkerClicked(int roomIndex)
        {
            _targetRoom = roomIndex;
            _targetPos = Camera.main.WorldToScreenPoint(levelMapVariable.Value.rooms[roomIndex].pos);
            _onUpdate = MoveToTarget;
        }

        private void MoveToTarget()
        {
            if (Vector3.Distance(transform.position, _targetPos) > eps)
            {
                transform.position += (_targetPos - transform.position).normalized * (Time.deltaTime * speed);
            }
            else
            {
                RoomReached();
            }
        }

        private void RoomReached()
        {
            _onUpdate = null;
            transform.position = _targetPos;
            OnRoomReached?.Invoke(_targetRoom);
        }
    }
}