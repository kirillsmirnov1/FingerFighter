using FingerFighter.Control.Common.Combat.Health;
using UnityEngine;

namespace FingerFighter.Control.Common.Enemies
{
    public class KillOutOfScreen : MonoBehaviour
    {
        [SerializeField] private EnemyHealth health;

        private void OnBecameInvisible()
        {
            health.Change(-health.BaseHealth);
        }
    }
}