using FingerFighter.Model.Combat;
using UnityEngine;

namespace FingerFighter.Control.Combat.Health
{
    public class EnemyHealth : AHealth
    {
        [SerializeField] private CombatEntityId id;

        public override float BaseHealth => id.EnemyStats.health;
        
        private void OnValidate()
        {
            id = GetComponent<CombatEntityId>();
        }
    }
}