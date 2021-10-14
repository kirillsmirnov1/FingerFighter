using FingerFighter.Control.Factories;
using FingerFighter.Model.Damage;
using UnityEngine;

namespace FingerFighter.Control.Damage
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class HitTaker : MonoBehaviour
    {
        [SerializeField] public Affiliation affiliation;
        
        public void TakeAHit(float hitForce, Vector2 position, Vector2 direction)
        {
            DisplayHitDamage(hitForce, position, direction);
        }

        private static void DisplayHitDamage(float hitForce, Vector2 position, Vector2 direction)
        {
            FlyingTextFactory.Instance
                .Instantiate($"{hitForce:0}", position, direction);
        }
    }
}
