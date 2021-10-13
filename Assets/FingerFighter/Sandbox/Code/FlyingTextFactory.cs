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
            if (direction.sqrMagnitude < 0.001f) direction = Random.insideUnitCircle;
            Instantiate(hitDamageTextPrefab, position, Quaternion.identity, transform)
                .GetComponent<FlyingText>()
                .Init(text, direction);
        }
    }
}