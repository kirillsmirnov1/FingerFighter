using FingerFighter.Control.Common.Combat.Health;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FingerFighter.Control.Runner
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
