using FingerFighter.Model.LevelMaps;
using FingerFighter.View.LevelMaps.Player;
using UnityEngine;
using UnityUtils;

namespace FingerFighter.Control.LevelMaps
{
    public class LevelMapFlow : MonoBehaviour
    {
        [SerializeField] private RoomsStatus roomsStatus;
        
        // TODO on boss defeat — move next 
        private void OnValidate()
        {
            this.CheckNullFields();
        }

        private void Awake()
        {
            PlayerMarker.OnRoomReached += OnPlayerReachedRoom;
        }

        private void OnDestroy()
        {
            PlayerMarker.OnRoomReached -= OnPlayerReachedRoom;
        }

        private void OnPlayerReachedRoom(int roomIndex)
        {
            switch (roomsStatus[roomIndex])
            {
                case RoomStatus.UnTouched:
                case RoomStatus.NextTarget:
                    UseRoom(roomIndex);
                    break;
                case RoomStatus.Used:
                    break;
            }
        }

        private void UseRoom(int roomIndex)
        {
            // TODO actually run room 
            roomsStatus[roomIndex] = RoomStatus.Used;
        }
    }
}