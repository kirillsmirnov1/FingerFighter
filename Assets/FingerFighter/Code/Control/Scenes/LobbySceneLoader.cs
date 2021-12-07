using FingerFighter.Model.LevelMaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityUtils.Scenes;
using UnityUtils.Variables;

namespace FingerFighter.Control.Scenes
{
    public class LobbySceneLoader : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private LevelMapVariable levelVariable;
        [SerializeField] private IntVariable currentRoom;
        [SerializeField] private RoomsStatus roomsStatus;

        [Header("Scenes")]
        [SerializeField] private SceneNameReference ring;
        [SerializeField] private SceneNameReference map;
        [SerializeField] private SceneNameReference runner;

        private void OnValidate()
        {
#if UNITY_EDITOR
            ring.SerializeName();
            map.SerializeName();
            runner.SerializeName();
#endif
        }

        private void Start()
        {
            PickSceneAndLoad();
        }

        private void PickSceneAndLoad()
        {
            var levelMap = levelVariable.Value;
            string sceneName;

            if (levelMap == null || levelMap.rooms == null || levelMap.rooms.Count == 0)
            {
                sceneName = ring.sceneName;
            }
            else
            {
                if (roomsStatus[currentRoom] == RoomStatus.Used)
                {
                    sceneName = map.sceneName;
                }
                else
                {
                    sceneName = runner.sceneName;
                } 
            }
            SceneManager.LoadScene(sceneName);
        }
    }
}
