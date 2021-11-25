using FingerFighter.Utils;
using UnityEngine;

namespace FingerFighter.Control.Enemies.Behaviour.Projectiles
{
    public class AliveProjectile : Projectile
    {
        protected override void Apply()
        {
            rb.AddAcceleration(Impulse * CombatTimeScale);
            var impulseChangeMod = Mathf.Clamp01(1f - rb.drag * CombatTimeScale * Time.deltaTime); 
            Impulse = impulseChangeMod * Impulse;
        }
    }
}