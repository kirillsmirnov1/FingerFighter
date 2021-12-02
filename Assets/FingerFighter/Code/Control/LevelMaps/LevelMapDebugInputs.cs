using FingerFighter.View.LevelMaps;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FingerFighter.Control.LevelMaps
{
    public class LevelMapDebugInputs : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private RoomsGraphGenerator levelMapGenerator;
        [SerializeField] private LevelMapDisplay levelMapDisplay;

        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                levelMapGenerator.Generate();
                levelMapDisplay.SpawnMap();
            }
        }
#endif
    }
}