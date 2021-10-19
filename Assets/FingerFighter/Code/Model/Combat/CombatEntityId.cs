﻿using FingerFighter.Model.Damage;
using UnityEngine;
using UnityUtils.Attributes;

namespace FingerFighter.Model.Combat
{
    public class CombatEntityId : MonoBehaviour
    {
        [SerializeField] private Affiliation affiliation = Affiliation.Nada;

        [ConditionalField("affiliation", compareValues:new object[] {Affiliation.Enemy})] 
        [SerializeField] private EnemyStats stats;  

        public string EnemyType => stats.tag;
        public Affiliation Affiliation => affiliation;
        public EnemyStats EnemyStats => stats;
        
        private void OnValidate()
        {
            Check(AffiliationNotSet && IsNotCombatEntityPrefab, 
                $"Set affiliation on {gameObject.name}");
            
            Check(IsEnemy && IsNotBaseEnemyPrefab && NoStatsForThatEnemy, 
                $"Provide enemy data for {gameObject.name}");        
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