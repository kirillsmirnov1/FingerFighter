using FingerFighter.Model.Damage;
using UnityEngine;
using UnityUtils.Attributes;

namespace FingerFighter.Model.Combat
{
    public class CombatEntityId : MonoBehaviour
    {
        [SerializeField] private Affiliation affiliation = Affiliation.Nada;
        [ConditionalField("affiliation", compareValues:new object[] {Affiliation.Enemy})]
        [SerializeField] private string enemyType;
        
        [ConditionalField("affiliation", compareValues:new object[] {Affiliation.Enemy})] 
        [SerializeField] private EnemyStatsData stats;

        public string EnemyType => enemyType;
        public Affiliation Affiliation => affiliation; 
        public EnemyStats EnemyStats { get; private set; }
        
        private void OnValidate()
        {
            Check(AffiliationNotSet && IsNotCombatEntityPrefab, 
                $"Set affiliation on {gameObject.name}");
            
            Check(IsEnemy && EnemyTypeNotSet && IsNotBaseEnemyPrefab, 
                $"Set enemy type on {gameObject.name}");
            
            Check(IsEnemy && !EnemyTypeNotSet && NoStatsForThatEnemy, 
                $"Provide enemy data for {gameObject.name}");        
        }

        private static void Check(bool condition, string warning)
        {
            if(condition) Debug.LogWarning(warning);
        }

        private void Awake()
        {
            if(IsEnemy) EnemyStats = stats.GetData(enemyType);
        }

        private bool AffiliationNotSet => affiliation == Affiliation.Nada;
        private bool IsNotCombatEntityPrefab => gameObject.name != "CombatEntity";
        private bool IsEnemy => affiliation == Affiliation.Enemy;
        private bool EnemyTypeNotSet => string.IsNullOrEmpty(enemyType);
        private bool IsNotBaseEnemyPrefab => gameObject.name != "_EnemyCombatEntity";
        private bool NoStatsForThatEnemy => !stats.HasEnemy(enemyType);
    }
}