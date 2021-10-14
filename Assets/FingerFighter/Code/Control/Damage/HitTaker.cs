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
        
        public void TakeAHit(HitData hitData)
        {
            DisplayHitDamage(hitData);
        }

        private static void DisplayHitDamage(HitData hitData)
        {
            FlyingTextFactory.Instance
                .Instantiate($"{hitData.Force:0}", hitData.Position, hitData.Direction);
        }
    }
}
