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

        protected virtual void OnEnable() => health.onNoHealth += OnNoHealth;
        protected virtual void OnDisable() => health.onNoHealth -= OnNoHealth;
        private void OnNoHealth() => gameObject.SetActive(false);
    }
}