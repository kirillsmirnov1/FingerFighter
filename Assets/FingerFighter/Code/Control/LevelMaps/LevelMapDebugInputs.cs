using FingerFighter.Model.LevelMaps;
using FingerFighter.View.LevelMaps;
using FingerFighter.View.LevelMaps.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FingerFighter.Control.LevelMaps
{
    public class LevelMapDebugInputs : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private LevelMapGenerator levelMapGenerator;
        [SerializeField] private LevelMapDisplay levelMapDisplay;
        [SerializeField] private PlayerMarker playerMarker;
        [SerializeField] private RoomsStatus roomsStatus;

        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                levelMapGenerator.Generate();
                levelMapDisplay.SpawnMap();
                playerMarker.SetPosition(0);
            }

            if (Keyboard.current.uKey.wasPressedThisFrame)
            {
                for (int i = 0; i < roomsStatus.Length; i++)
                {
                    roomsStatus[i] = RoomStatus.Used;
                }
            }
        }
#endif
    }
}