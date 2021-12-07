using System;
using FingerFighter.Model.Combat.Flow;
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
        [SerializeField] private RunnerFlowContainer roomFlow;
        [SerializeField] private RunnerFlowContainerVariable runnerFlowVar;
        [SerializeField] private LevelMapVariable levelMapVariable;
        
        [Header("Scenes")]
        [SerializeField] private SceneNameReference runner;
        [SerializeField] private SceneNameReference ring;
        
        private void OnValidate()
        {
            this.CheckNullFields();
            runner.SerializeName();
            ring.SerializeName();
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

        private void OnEnable()
        {
            CheckEmptyMap();
            CheckBossFinished();
        }

        private void CheckEmptyMap()
        {
            var level = levelMapVariable.Value;
            if (level?.rooms == null || level.rooms.Count == 0)
            {
                SceneManager.LoadScene(ring.sceneName);
            }
        }

        private void CheckBossFinished()
        {
            var bossRoomIndex = levelMapVariable.Value.rooms.Count - 1;
            if (roomsStatus[bossRoomIndex] == RoomStatus.Used)
            {
                OnBossDefeated();
            }
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
                    UseRoom();
                    break;
                case RoomStatus.Used:
                    var room = levelMapVariable.Value.rooms[roomIndex];
                    switch (room.type)
                    {
                        case RoomType.Start:
                        case RoomType.Regular:
                            break;
                        case RoomType.Boss:
                            OnBossDefeated();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UseRoom()
        {
            runnerFlowVar.Value = roomFlow;
            SceneManager.LoadScene(runner.sceneName);
        }

        private void OnBossDefeated()
        {
            // TODO show results 
            // TODO allow to go next 
            levelMapVariable.Value = null;
            SceneManager.LoadScene(ring.sceneName);
        }
    }
}