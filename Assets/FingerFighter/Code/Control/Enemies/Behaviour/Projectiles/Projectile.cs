using FingerFighter.Control.Combat.Damage;
using FingerFighter.Utils;
using UnityEngine;

namespace FingerFighter.Control.Enemies.Behaviour.Projectiles
{
    public class Projectile : AEnemyBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ProjectileHitProvider hitProvider;
        
        private float _angle;
        protected Vector2 Impulse;

        protected override void OnValidate()
        {
            base.OnValidate();
            spriteRenderer = GetComponent<SpriteRenderer>();
            hitProvider = GetComponent<ProjectileHitProvider>();
        }

        public void Init(GameObject parentTurret, Color projectileColor, Vector2 impulse, float angle)
        {
            _angle = angle;
            if(hitProvider!= null) hitProvider.parentTurret = parentTurret;
            spriteRenderer.color = projectileColor;
            Impulse = impulse;
            gameObject.SetActive(true);
        }

        protected override void Apply()
        {
            Rb.rotation = _angle;
            Rb.AddAcceleration(Impulse * CombatTimeScale);
        }
    }
}