using UnityEngine;

namespace FingerFighter.Sandbox
{
    public class FlyingTextFactory : MonoBehaviour
    {
        public static FlyingTextFactory Instance;

        [SerializeField] private GameObject hitDamageTextPrefab;
        
        private void Awake()
        {
            Instance = this;
        }

        public void Instantiate(string text, Vector2 position, Vector2 direction)
        {
            direction = NormalizeDirection(direction);
            Instantiate(hitDamageTextPrefab, position, Quaternion.identity, transform)
                .GetComponent<FlyingText>()
                .Init(text, direction);
        }

        private static Vector2 NormalizeDirection(Vector2 direction)
        {
            if (direction.sqrMagnitude < 0.001f) direction = new Vector2(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f));
            return direction.normalized;
        }
    }
}