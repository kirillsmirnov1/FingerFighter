using FingerFighter.Model.Damage;
using FingerFighter.Model.Damage.HitDataProvider;
using UnityEngine;

namespace FingerFighter.Control.Damage
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class HitProvider : MonoBehaviour
    {
        [SerializeField] private Affiliation affiliation;
        [SerializeField] private AHitDataProvider hitDataProvider;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            var hitTaker = other.gameObject.GetComponent<HitTaker>();
            
            if (hitTaker == null) return;
            if (hitTaker.affiliation == affiliation) return;
            
            hitTaker.TakeAHit(PrepareHitData(other, hitTaker));
        }

        private HitData PrepareHitData(Collision2D hitTakerCollision, HitTaker hitTaker)
        {
            var hitData = hitDataProvider.HitData;
            if (hitData.Direction.sqrMagnitude < 0.001f)
            {
                hitData.Direction = hitTakerCollision.transform.position - transform.position;
            }
            hitData.Direction.Normalize();
            hitData.Position = hitTakerCollision.contacts[0].point;
            hitData.Affected = hitTaker.affiliation;
            return hitData;
        }
    }
}
