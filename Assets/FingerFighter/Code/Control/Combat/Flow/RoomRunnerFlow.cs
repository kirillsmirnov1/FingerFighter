using System.Collections.Generic;
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
            // TODO switch on boss
            var formationIds = levelMapVariable.Value.rooms[currentRoom].formations;
            formations = new Queue<EnemyFormation>(
                enemyProvider.GetFormations(currentLevel, formationIds)); 
        }

        protected override void OnNoFormationsLeft()
        {
            // TODO consider it a win — show results 
            roomsStatus[currentRoom] = RoomStatus.Used;
            SceneManager.LoadScene(levelMapScene.sceneName);
        }
    }
}