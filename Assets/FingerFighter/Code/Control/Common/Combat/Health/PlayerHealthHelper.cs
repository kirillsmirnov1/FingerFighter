using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Control.Common.Combat.Health
{
    [CreateAssetMenu(menuName = "Control/PlayerHealthHelper", fileName = "PlayerHealthHelper", order = 0)]
    public class PlayerHealthHelper : ScriptableObject
    {
        [SerializeField] private FloatVariable baseHealth;
        [SerializeField] private FloatVariable currentHealth;

        public void Revive()
        {
            currentHealth.Value = baseHealth;
        }
    }
}