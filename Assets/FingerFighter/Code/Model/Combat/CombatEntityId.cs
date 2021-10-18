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
        
        public string EnemyType => enemyType;
        public Affiliation Affiliation => affiliation; 
        
        private void OnValidate()
        {
            if (affiliation == Affiliation.Nada && gameObject.name != "CombatEntity")
            {
                Debug.Log($"Set affiliation on {gameObject.name}");
            }

            if (affiliation == Affiliation.Enemy && string.IsNullOrEmpty(enemyType))
            {
                Debug.Log($"Set enemy type on {gameObject.name}");
            }
        }
    }
}