using FingerFighter.Control.Character.Handles;
using FingerFighter.Model.Damage;
using UnityEngine;

namespace FingerFighter.Control.Damage
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class HitProvider : MonoBehaviour
    {
        [SerializeField] private Affiliation affiliation;
        [SerializeField] private HandleSpeed handleSpeed; // IMPR IHitForceProvider for both player and enemy
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            var hitTaker = other.gameObject.GetComponent<HitTaker>();
            
            if (hitTaker == null) return;
            if (hitTaker.affiliation == affiliation) return;
            
            var contactPosition = other.contacts[0].point; 
            hitTaker.TakeAHit(
                handleSpeed.Speed, 
                contactPosition, 
                handleSpeed.Direction);
        }
    }
}
