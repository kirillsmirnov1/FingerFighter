﻿using FingerFighter.Control.Combat.Health;
using FingerFighter.Model.Combat;
using UnityEngine;

namespace FingerFighter.Control.Combat.Status
{
    [RequireComponent(typeof(AHealth))]
    [RequireComponent(typeof(CombatEntityId))]
    public abstract class CombatEntityStatus : MonoBehaviour
    {
        [SerializeField] private AHealth health;
        [SerializeField] protected CombatEntityId id;

        private void OnValidate()
        {
            if (health == null) health = GetComponent<AHealth>();
            if (id == null) id = GetComponent<CombatEntityId>();
        }

        protected virtual void OnEnable() => health.onNoHealth += OnNoHealth;
        protected virtual void OnDisable()
        {
            health.onNoHealth -= OnNoHealth;
            if (health.NoHealth)
            {
                OnEntityDeath();
            }
        }

        protected abstract void OnEntityDeath();

        private void OnNoHealth() => gameObject.SetActive(false);
    }
}