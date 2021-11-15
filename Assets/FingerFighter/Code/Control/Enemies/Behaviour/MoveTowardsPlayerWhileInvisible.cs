using System;
using FingerFighter.Model.Combat;
using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Control.Enemies.Behaviour
{
    public class MoveTowardsPlayerWhileInvisible : MonoBehaviour
    {
        [SerializeField] private TransformVariable player;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private CombatEntityId id;

        private EnemyStats _stats;
        private Action _onFixedUpdate;
        private Transform _player;
        private Transform _self;

        private void OnValidate()
        {
            rb = GetComponent<Rigidbody2D>();
            id = GetComponent<CombatEntityId>();
        }

        private void Awake()
        {
            OnBecameInvisible();
            _player = player.Value;
            _self = transform;
            _stats = id.EnemyStats;
        }

        private void OnBecameVisible() 
            => _onFixedUpdate = null;
        private void OnBecameInvisible() 
            => _onFixedUpdate = Move;

        private void FixedUpdate()
        {
            _onFixedUpdate?.Invoke();
        }
        
        
        // TODO Tweak until it works
        private void Move()
        {
            Vector2 direction = (_player.position - _self.position).normalized;
            rb.velocity += _stats.movementSpeed * direction;
        }
    }
}