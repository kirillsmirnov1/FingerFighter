using System;
using FingerFighter.Model;
using FingerFighter.Model.Damage;
using UnityEngine;

namespace FingerFighter.Control.Damage
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class HitTaker : MonoBehaviour
    {
        public static event Action<HitData> OnHitTaken; 
        
        [SerializeField] public Affiliation affiliation;
        [SerializeField] private Health health;

        public void TakeAHit(HitData hitData)
        {
            health.Change(-hitData.Force); 
            OnHitTaken?.Invoke(hitData);
        }
    }
}
