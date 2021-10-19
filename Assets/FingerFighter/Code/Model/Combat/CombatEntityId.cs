using System;
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
            if (affiliation == Affiliation.Nada && gameObject.name != "CombatEntity")
            {
                Debug.LogWarning($"Set affiliation on {gameObject.name}");
            }

            if (IsEnemy && string.IsNullOrEmpty(enemyType) && gameObject.name != "_EnemyCombatEntity")
            {
                Debug.LogWarning($"Set enemy type on {gameObject.name}");
            }

            if (IsEnemy && !string.IsNullOrEmpty(enemyType) && !stats.HasEnemy(enemyType))
            {
                Debug.LogWarning($"Provide enemy data for {gameObject.name}");
            }
        }

        private void Awake()
        {
            if(IsEnemy) EnemyStats = stats.GetData(enemyType);
        }

        private bool IsEnemy => affiliation == Affiliation.Enemy;
    }
}