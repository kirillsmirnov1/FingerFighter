using FingerFighter.Model.Common.Enemies;
using UnityEngine;

namespace FingerFighter.Control.Common.Combat.Health
{
    public class EnemyHealth : AHealth
    {
        [SerializeField] private EnemyComponents components;

        public override float BaseHealth => components.stats.health;
        
        private void OnValidate()
        {
            components = GetComponent<EnemyComponents>();
        }
    }
}