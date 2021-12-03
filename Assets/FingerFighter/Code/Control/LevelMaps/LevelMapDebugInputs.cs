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
        
        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                levelMapGenerator.Generate();
                levelMapDisplay.SpawnMap();
                playerMarker.SetPosition(0);
            }
        }
#endif
    }
}