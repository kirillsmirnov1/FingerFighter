using System;
using System.Collections.Generic;
using System.ComponentModel;
using FingerFighter.Model.EnemyFormations;
using FingerFighter.Model.LevelMaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityUtils.Scenes;
using UnityUtils.Variables;

namespace FingerFighter.Control.Combat.Flow
{
    [CreateAssetMenu(menuName = "Model/Flow/RoomRunnerFlow", fileName = "RoomRunnerFlow", order = 0)]
    public class RoomRunnerFlow : ARunnerFlow
    {
        [SerializeField] private StringVariable currentLevel;
        [SerializeField] private IntVariable currentRoom;
        [SerializeField] private LevelMapVariable levelMapVariable;
        [SerializeField] private RoomsStatus roomsStatus;
        [SerializeField] private SceneNameReference levelMapScene;
        
        // TODO handle total death — drop progress 
        
        private void OnValidate()
        {
            levelMapScene.SerializeName();
        }

        protected override void UpdateFormationsQueue()
        {
            currentPack = currentLevel.Value;
            var room = levelMapVariable.Value.rooms[currentRoom];
            var formationData = room.type switch
            {
                RoomType.Regular => enemyProvider.GetFormations(currentPack, room.formations),
                RoomType.Boss => enemyProvider.GetBossFormation(currentPack),
                RoomType.Start => throw new InvalidEnumArgumentException(),
                _ => throw new ArgumentOutOfRangeException()
            };
            formations = new Queue<EnemyFormation>(formationData); 
        }

        protected override void OnNoFormationsLeft()
        {
            // TODO consider it a win — show results 
            roomsStatus[currentRoom] = RoomStatus.Used;
            SceneManager.LoadScene(levelMapScene.sceneName);
        }
    }
}