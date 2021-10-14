using System.Collections.Generic;
using FingerFighter.View;
using UnityEngine;

namespace FingerFighter.Control.Factories
{
    public class FlyingTextFactory : MonoBehaviour
    {
        public static FlyingTextFactory Instance;

        [SerializeField] private GameObject hitDamageTextPrefab;
        
        private readonly Queue<FlyingText> _pool = new Queue<FlyingText>();
        
        private void Awake()
        {
            Instance = this;
        }

        public void Instantiate(string text, Vector2 position, Vector2 direction) // IMPR use struct FlyingTextData
        {
            var newFlyingText = GetNewFlyingText(position);
            newFlyingText.Init(text, direction);
        }

        private FlyingText GetNewFlyingText(Vector2 position)
        {
            if (_pool.Count > 0)
            {
                var newFlyingText = _pool.Dequeue();
                newFlyingText.transform.position = position;
                newFlyingText.gameObject.SetActive(true);
                return newFlyingText;
            }
            else
            {
                return Instantiate(hitDamageTextPrefab, position, Quaternion.identity, transform)
                    .GetComponent<FlyingText>();
            }
        }

        public void ReturnToPool(FlyingText flyingText)
        {
            flyingText.gameObject.SetActive(false);
            _pool.Enqueue(flyingText);
        }
    }
}