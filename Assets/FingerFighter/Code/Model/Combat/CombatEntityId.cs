using FingerFighter.Model.Damage;
using UnityEngine;

namespace FingerFighter.Model.Combat
{
    public class CombatEntityId : MonoBehaviour
    {
        [field: SerializeField] public Affiliation Affiliation { get; private set; } = Affiliation.Nada;

        private void OnValidate()
        {
            if (Affiliation == Affiliation.Nada && gameObject.name != "CombatEntity")
            {
                Debug.Log($"Set affiliation on {gameObject.name}");
            }
        }
    }
}