using FingerFighter.Control.Enemies.Behaviour.Projectiles;
using FingerFighter.Model.Combat.Damage;
using UnityEngine;
using UnityUtils.Attributes;
using UnityUtils.Variables;

namespace FingerFighter.Model.Combat
{
    public class CombatEntityId : MonoBehaviour
    {
        [SerializeField] private Affiliation affiliation = Affiliation.Nada;

        [ConditionalField("affiliation", compareValues:new object[] {Affiliation.Enemy})] 
        [SerializeField] private EnemyStats stats;

        [Header("Fields")]
        [SerializeField] public Rigidbody2D rb;
        [ConditionalField("affiliation", compareValues:new object[] {Affiliation.Enemy})] 
        [SerializeField] public Projectile projectile;
        [SerializeField] public FloatVariable combatTimeScale;

        public string EnemyType => stats.tag;
        public Affiliation Affiliation => affiliation;
        public EnemyStats EnemyStats => stats;
        
        private void OnValidate()
        {
            Check(AffiliationNotSet && IsNotCombatEntityPrefab, 
                $"Set affiliation on {gameObject.name}");
            
            Check(IsEnemy && IsNotBaseEnemyPrefab && NoStatsForThatEnemy, 
                $"Provide enemy data for {gameObject.name}");

            rb = GetComponent<Rigidbody2D>();
        }

        private static void Check(bool condition, string warning)
        {
            if (condition) Debug.LogWarning(warning);
        }

        private bool AffiliationNotSet => affiliation == Affiliation.Nada;
        private bool IsNotCombatEntityPrefab => gameObject.name != "CombatEntity";
        private bool IsEnemy => affiliation == Affiliation.Enemy;
        private bool IsNotBaseEnemyPrefab => gameObject.name != "_EnemyCombatEntity";
        private bool NoStatsForThatEnemy => stats == null;
    }
}