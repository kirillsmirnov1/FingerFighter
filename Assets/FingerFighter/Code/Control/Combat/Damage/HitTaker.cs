using System;
using FingerFighter.Control.Combat.Health;
using FingerFighter.Control.Conditions.Flags;
using FingerFighter.Model.Combat;
using FingerFighter.Model.Combat.Damage;
using UnityEngine;
using UnityUtils;

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
        [SerializeField] private AFlag[] damageBlockers;

        public Affiliation Affiliation { get; private set; }

        private void OnEnable()
        {
            Affiliation = id.Affiliation;
        }

        public void TakeAHit(HitData hitData)
        {
            if(DamageBlocked) return;

            health.Change(-hitData.Force); 
            onHitTaken?.Invoke(hitData);
            OnHitTaken?.Invoke(hitData);
        }

        private bool DamageBlocked
        {
            get
            {
                for (var i = 0; i < damageBlockers.Length; i++)
                {
                    if(damageBlockers[i].On) return true;
                }
                return false;
            }
        }
    }
}
