using FingerFighter.Model;
using FingerFighter.Model.Combat;
using UnityEngine;

namespace FingerFighter.Control.Combat
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(CombatEntityId))]
    public class CombatEntityStatus : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private CombatEntityId id;

        private void OnValidate()
        {
            if (health == null) health = GetComponent<Health>();
            if (id == null) id = GetComponent<CombatEntityId>();
        }

        private void OnEnable() => health.onNoHealth += OnNoHealth;
        private void OnDisable() => health.onNoHealth -= OnNoHealth;
        private void OnNoHealth() => Die();
        private void Die() => Destroy(gameObject); // IMPR probably should provide it with some referenced behaviour 
    }
}