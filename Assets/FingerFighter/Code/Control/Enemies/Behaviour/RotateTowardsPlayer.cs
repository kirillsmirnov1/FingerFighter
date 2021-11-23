﻿using UnityEngine;
using UnityUtils.Extensions;

namespace FingerFighter.Control.Enemies.Behaviour
{
    public class RotateTowardsPlayer : AEnemyBehaviour
    {
        [SerializeField] public float angleOffset = -90;

        private void OnEnable()
        {
            Self.rotation = DesiredRotation;
        }

        protected override void Apply()
        {
            var nextRotation = Quaternion.Slerp(Self.rotation, DesiredRotation, Stats.rotationSpeed); 
            Self.rotation = nextRotation;
        }

        private Quaternion DesiredRotation 
            => QuaternionExt.LookRotation2D(DirectionToPlayer, angleOffset);

        private Vector2 DirectionToPlayer 
            => (Player.position - Self.position).normalized;
    }
}