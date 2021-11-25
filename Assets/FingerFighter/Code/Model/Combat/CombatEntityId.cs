using FingerFighter.Model.Combat.Damage;
using UnityEngine;

namespace FingerFighter.Model.Combat
{
    public class CombatEntityId : MonoBehaviour
    {
        [SerializeField] private Affiliation affiliation = Affiliation.Nada;

        public Affiliation Affiliation => affiliation;
        
        private void OnValidate()
        {
            Check(AffiliationNotSet && IsNotCombatEntityPrefab, 
                $"Set affiliation on {gameObject.name}");
        }

        private static void Check(bool condition, string warning)
        {
            if (condition) Debug.LogWarning(warning);
        }

        private bool AffiliationNotSet => affiliation == Affiliation.Nada;
        private bool IsNotCombatEntityPrefab => gameObject.name != "CombatEntity";
    }
}