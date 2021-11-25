using FingerFighter.Model.Enemies;
using UnityEngine;

namespace FingerFighter.Control.Combat.Health
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