using System;
using FingerFighter.Control.Enemies.Behaviour.Projectiles;
using FingerFighter.Model.Combat;
using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Model.Enemies
{
    public class EnemyComponents : MonoBehaviour
    {
        [SerializeField] public CombatEntityId id;
        [SerializeField] public Rigidbody2D rb;
        [SerializeField] public Projectile projectile;
        
        [Header("Data")]
        [SerializeField] protected TransformVariable player;
        [SerializeField] public FloatVariable combatTimeScale;
        public Transform Target { get; private set; }

        private void OnValidate()
        {
            id ??= GetComponent<CombatEntityId>();
            rb ??= GetComponent<Rigidbody2D>();
            projectile ??= GetComponent<Projectile>();
        }

        private void Awake()
        {
            Target = player.Value;
        }

        public void OverrideTarget(Transform target)
            => Target = target;
    }
}