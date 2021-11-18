﻿using FingerFighter.Model.Combat;
using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Control.Enemies.Behaviour
{
    public abstract class AEnemyBehaviour : MonoBehaviour
    {
        [SerializeField] private CombatEntityId id;
        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected TransformVariable player;

        protected EnemyStats Stats => id.EnemyStats;

        protected Transform Self;
        protected Transform Player;
        
        protected virtual void Awake()
        {
            Self = transform;
            Player = player.Value;
        }

        private void OnValidate()
        {
            id ??= GetComponent<CombatEntityId>();
            rb ??= GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() => Apply();

        protected abstract void Apply();
    }
}