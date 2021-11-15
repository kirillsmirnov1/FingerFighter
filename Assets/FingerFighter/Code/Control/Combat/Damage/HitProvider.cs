using FingerFighter.Model.Combat;
using FingerFighter.Model.Combat.Damage;
using FingerFighter.Model.Combat.Damage.HitDataProvider;
using UnityEngine;

namespace FingerFighter.Control.Combat.Damage
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class HitProvider : MonoBehaviour
    {
        [SerializeField] private CombatEntityId id;
        [SerializeField] private AHitDataProvider hitDataProvider;

        private Affiliation _affiliation;

        private void OnEnable()
        {
            _affiliation = id.Affiliation;
        }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            var hitTaker = other.gameObject.GetComponent<HitTaker>();
            
            if (hitTaker == null) return;
            if (hitTaker.Affiliation == _affiliation) return;

            var hitData = PrepareHitData(other, hitTaker);
            if(hitData.Force < 1f) return;
            hitTaker.TakeAHit(hitData);
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
            hitData.Affected = hitTaker.Affiliation;
            return hitData;
        }
    }
}
