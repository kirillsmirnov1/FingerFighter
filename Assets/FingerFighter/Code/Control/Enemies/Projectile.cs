using FingerFighter.Control.Combat.Damage;
using UnityEngine;

namespace FingerFighter.Control.Enemies
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ProjectileHitProvider hitProvider;
        [SerializeField] private Rigidbody2D rb;
        
        private float _angle;

        private void OnValidate()
        {
            hitProvider = GetComponent<ProjectileHitProvider>();
            rb = GetComponent<Rigidbody2D>();
        }

        public void Init(GameObject parentTurret, Vector2 impulse, float angle)
        {
            _angle = angle;
            hitProvider.parentTurret = parentTurret;
            gameObject.SetActive(true);
            rb.AddForce(impulse, ForceMode2D.Impulse);
        }

        private void FixedUpdate()
        {
            rb.rotation = _angle;
        }
    }
}