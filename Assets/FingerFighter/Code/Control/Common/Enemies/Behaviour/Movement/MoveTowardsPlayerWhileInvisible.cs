﻿using System;
using FingerFighter.Utils;
using UnityEngine;

namespace FingerFighter.Control.Common.Enemies.Behaviour.Movement
{
    public class MoveTowardsPlayerWhileInvisible : AEnemyBehaviour
    {
        private Action _action;

        protected override void Awake()
        {
            base.Awake();
            OnBecameInvisible();
        }

        private void OnBecameVisible() 
            => _action = null;
        private void OnBecameInvisible() 
            => _action = Move;

        protected override void Apply() 
            => _action?.Invoke();

        private void Move()
        {
            Vector2 direction = (Target.position - Self.position).normalized;
            var acceleration = direction * (CombatTimeScale * MovementSpeed);
            Rb.AddAcceleration(acceleration);
        }
    }
}