using FingerFighter.Control.Combat.Health;
using UnityEngine;

namespace FingerFighter.Control.Enemies
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