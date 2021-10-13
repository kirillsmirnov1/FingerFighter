using UnityEngine;

namespace FingerFighter.Sandbox
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class HitTaker : MonoBehaviour
    {
        [SerializeField] private GameObject hitDamageTextPrefab;
        
        public void TakeAHit(float hitForce, Vector2 position, Vector2 direction)
        {
            Instantiate(hitDamageTextPrefab, position, Quaternion.identity)
                .GetComponent<HitDamageText>()
                .Init(hitForce, direction);
        }
    }
}
