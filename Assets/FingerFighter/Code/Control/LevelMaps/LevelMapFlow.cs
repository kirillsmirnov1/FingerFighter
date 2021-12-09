using System;
using FingerFighter.Control.Scenes;
using FingerFighter.Model.Combat.Flow;
using FingerFighter.Model.EnemyFormations;
using FingerFighter.Model.LevelMaps;
using FingerFighter.View.LevelMaps;
using FingerFighter.View.LevelMaps.Player;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils;
using UnityUtils.Scenes;
using UnityUtils.Variables;

namespace FingerFighter.Control.LevelMaps
{
    public class LevelMapFlow : MonoBehaviour
    {
        [SerializeField] private RoomsStatus roomsStatus;
        [SerializeField] private RunnerFlowContainer roomFlow;
        [SerializeField] private RunnerFlowContainerVariable runnerFlowVar;
        [SerializeField] private LevelMapVariable levelMapVariable;
        [SerializeField] private EnemyFormationPackArray packs;
        [SerializeField] private StringVariable levelId;
        [SerializeField] private LevelMapGenerator levelMapGenerator;
        [SerializeField] private IntVariable playerPos;
        
        [Header("Components")]
        [SerializeField] private Button goNextButton;

        [Header("Scenes")]
        [SerializeField] private SceneNameReference runner;
        [SerializeField] private SceneNameReference ring;
        
        private void OnValidate()
        {
            this.CheckNullFieldsIfNotPrefab();
#if UNITY_EDITOR
            runner.SerializeName();
            ring.SerializeName();
#endif
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
                SceneManagerCustom.LoadScene(ring.sceneName);
            }
        }

        private void CheckBossFinished()
        {
            var bossRoomIndex = levelMapVariable.Value.rooms.Count - 1;
            var bossDefeated = roomsStatus[bossRoomIndex] == RoomStatus.Used; 
            if (bossDefeated)
            {
                OnBossDefeated();
            }
            goNextButton.gameObject.SetActive(bossDefeated);
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
            SceneManagerCustom.LoadScene(runner.sceneName);
        }

        private void OnBossDefeated()
        {
            goNextButton.gameObject.SetActive(true);
            goNextButton.onClick.AddListener(OnGoingNextButtonClicked);
        }

        private void OnGoingNextButtonClicked()
        {
            // TODO show confirmation window 
            if (packs.HasNext(levelId))
            {
                levelId.Value = packs.GetNextId(levelId);
                levelMapGenerator.Generate();
                playerPos.Value = 0;
                SceneManagerCustom.Reload();
            }
            else
            {
                SceneManagerCustom.LoadScene(ring.sceneName);
            }
        }
    }
}