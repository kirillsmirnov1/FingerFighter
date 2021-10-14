using System;
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
        
        public void TakeAHit(HitData hitData)
        {
            // TODO damage entity 
            OnHitTaken?.Invoke(hitData);
        }
    }
}
