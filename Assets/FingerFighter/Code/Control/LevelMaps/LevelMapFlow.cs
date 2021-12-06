using FingerFighter.Model.LevelMaps;
using FingerFighter.View.LevelMaps;
using FingerFighter.View.LevelMaps.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityUtils;
using UnityUtils.Scenes;

namespace FingerFighter.Control.LevelMaps
{
    public class LevelMapFlow : MonoBehaviour
    {
        [SerializeField] private RoomsStatus roomsStatus;
        [SerializeField] private SceneNameReference runner;

        // TODO on boss defeat — move next 
        private void OnValidate()
        {
            this.CheckNullFields();
            runner.SerializeName();
        }

        private void Awake()
        {
            PlayerMarker.OnRoomReached += OnPlayerReachedRoom;
            RoomMarkerView.OnClick += OnRoomClicked;
        }

        private void OnDestroy()
        {
            PlayerMarker.OnRoomReached -= OnPlayerReachedRoom;
            RoomMarkerView.OnClick -= OnRoomClicked;
        }

        private void OnRoomClicked(int roomIndex)
        {
            if (roomsStatus[roomIndex] == RoomStatus.UnTouched)
            {
                roomsStatus[roomIndex] = RoomStatus.NextTarget;
            }
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
            // TODO set flow 
            SceneManager.LoadScene(runner.sceneName);
        }
    }
}