using FingerFighter.Utils;
using UnityEngine;

namespace FingerFighter.Control.Common.Enemies.Behaviour.Projectiles
{
    public class AliveProjectile : Projectile
    {
        protected override void Apply()
        {
            Rb.AddAcceleration(Impulse * CombatTimeScale);
            var impulseChangeMod = Mathf.Clamp01(1f - Rb.drag * CombatTimeScale * Time.deltaTime); 
            Impulse = impulseChangeMod * Impulse;
        }
    }
}