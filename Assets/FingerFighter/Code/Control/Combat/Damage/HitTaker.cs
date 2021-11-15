using System;
using FingerFighter.Control.Combat.Health;
using FingerFighter.Model.Combat;
using FingerFighter.Model.Combat.Damage;
using UnityEngine;

namespace FingerFighter.Control.Combat.Damage
{
    [RequireComponent(typeof(CombatEntityId))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class HitTaker : MonoBehaviour
    {
        public event Action<HitData> onHitTaken; 
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
            onHitTaken?.Invoke(hitData);
            OnHitTaken?.Invoke(hitData);
        }
    }
}
