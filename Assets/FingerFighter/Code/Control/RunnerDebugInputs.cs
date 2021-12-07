using FingerFighter.Control.Combat.Health;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FingerFighter.Control
{
    public class RunnerDebugInputs : MonoBehaviour
    {
#if UNITY_EDITOR
        private void Update()
        {
            if (Keyboard.current.kKey.wasPressedThisFrame)
            {
                KillAllEnemies();
            }
        }

        private void KillAllEnemies()
        {
            foreach (var enemyHealth in FindObjectsOfType<EnemyHealth>())
            {
                enemyHealth.Change(float.NegativeInfinity);
            }
        }
#endif
    }
}
