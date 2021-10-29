using System;
using System.Collections.Generic;
using FingerFighter.Control.Damage;
using FingerFighter.Model;
using FingerFighter.Model.Damage;
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
            HitTaker.OnHitTaken += OnHitTaken;
        }

        private void OnDestroy()
        {
            HitTaker.OnHitTaken -= OnHitTaken;
        }

        private void OnHitTaken(HitData hitData)
        {
            if(hitData.Force < 1) return;
            Instantiate(ComposeFlyingTextData(hitData));
        }

        private void Instantiate(FlyingTextData data)
        {
            var newFlyingText = GetNewFlyingText();
            newFlyingText.Init(data);
        }

        private FlyingText GetNewFlyingText()
        {
            if (_pool.Count > 0)
            {
                var newFlyingText = _pool.Dequeue();
                newFlyingText.gameObject.SetActive(true);
                return newFlyingText;
            }
            else
            {
                return Instantiate(hitDamageTextPrefab, transform)
                    .GetComponent<FlyingText>();
            }
        }

        private FlyingTextData ComposeFlyingTextData(HitData hitData)
        {
            return new FlyingTextData
            {
                Text = $"{hitData.Force:0}",
                TextColor = PickColor(hitData),
                Position = hitData.Position,
                Direction = hitData.Direction
            };
        }

        private Color PickColor(HitData hitData) =>
            hitData.Affected switch
            {
                Affiliation.Player => Color.red,
                Affiliation.Enemy => Color.white,
                _ => throw new ArgumentOutOfRangeException()
            };

        public void ReturnToPool(FlyingText flyingText)
        {
            flyingText.gameObject.SetActive(false);
            _pool.Enqueue(flyingText);
        }
    }
}