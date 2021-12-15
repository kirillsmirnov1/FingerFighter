using System;
using System.Collections.Generic;
using FingerFighter.Control.Common.Combat.Damage;
using FingerFighter.Control.Common.Combat.Status;
using FingerFighter.Model.Common;
using FingerFighter.Model.Common.Combat;
using FingerFighter.Model.Common.Combat.Damage;
using FingerFighter.Model.ViewData;
using FingerFighter.View.Common.TextColorAnim;
using UnityEngine;

namespace FingerFighter.Control.Common.Factories
{
    public class FlyingTextFactory : MonoBehaviour
    {
        public static FlyingTextFactory Instance;

        [SerializeField] private GameObject hitDamageTextPrefab;
        [SerializeField] private EnemyStatsList enemyStats;
        
        private readonly Queue<FlyingText> _pool = new Queue<FlyingText>();
        private readonly Vector2 _enemyDeathTextDirection = Vector2.up * 0.3f;

        private void Awake()
        {
            Instance = this;
            HitTaker.OnHitTaken += OnHitTaken;
            EnemyStatus.OnDeath += OnEnemyDeath;
        }

        private void OnDestroy()
        {
            HitTaker.OnHitTaken -= OnHitTaken;
            EnemyStatus.OnDeath -= OnEnemyDeath;
        }

        private void OnEnemyDeath(EnemyDeathData enemyDeathData)
        {
            if(enemyDeathData.IsSegment) return;
            if(enemyStats[enemyDeathData.Tag].points < 1) return;
            Instantiate(ComposeFlyingTextData(enemyDeathData.Tag, enemyDeathData.DeathPos));
        }

        private void OnHitTaken(HitData hitData)
        {
            if(hitData.Affected == Affiliation.Enemy) return;
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

        private FlyingTextData ComposeFlyingTextData(string enemyTag, Vector2 deathPos)
        {
            return new FlyingTextData
            {
                Text = $"+{enemyStats[enemyTag].points}",
                TextColor = Color.white,
                Position = deathPos,
                Direction = _enemyDeathTextDirection
            };
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