using FingerFighter.Control.Combat.Damage;
using UnityEngine;

namespace FingerFighter.Control.Enemies
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ProjectileHitProvider hitProvider;
        [SerializeField] private Rigidbody2D rb;
        
        private float _angle;

        private void OnValidate()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            hitProvider = GetComponent<ProjectileHitProvider>();
            rb = GetComponent<Rigidbody2D>();
        }

        public void Init(GameObject parentTurret, Color projectileColor, Vector2 impulse, float angle)
        {
            _angle = angle;
            hitProvider.parentTurret = parentTurret;
            spriteRenderer.color = projectileColor;
            gameObject.SetActive(true);
            rb.AddForce(impulse, ForceMode2D.Impulse);
        }

        private void FixedUpdate()
        {
            rb.rotation = _angle;
        }
    }
}