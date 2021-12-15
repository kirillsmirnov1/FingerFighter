using UnityEngine;

namespace FingerFighter.Control.Common.Combat.Damage
{
    public class ProjectileHitProvider : HitProvider
    {
        [HideInInspector] public GameObject parentTurret;
        
        protected override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
            if(other.gameObject == parentTurret) return;
            gameObject.SetActive(false);
        }
    }
}