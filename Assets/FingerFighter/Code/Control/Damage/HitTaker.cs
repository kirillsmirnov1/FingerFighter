using System;
using FingerFighter.Control.Combat.Health;
using FingerFighter.Model;
using FingerFighter.Model.Combat;
using FingerFighter.Model.Damage;
using UnityEngine;

namespace FingerFighter.Control.Damage
{
    [RequireComponent(typeof(CombatEntityId))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class HitTaker : MonoBehaviour
    {
        public static event Action<HitData> OnHitTaken;

        [SerializeField] private CombatEntityId id;
        [SerializeField] private AHealth health;

        public Affiliation Affiliation { get; private set; }

        private void OnEnable()
        {
            Affiliation = id.Affiliation;
        }

        public void TakeAHit(HitData hitData)
        {
            health.Change(-hitData.Force); 
            OnHitTaken?.Invoke(hitData);
        }
    }
}
