using FingerFighter.Model.Util;
using UnityEngine;

namespace FingerFighter.Control.Enemies.Behaviour
{
    public class RotateTowardsPlayer : AEnemyBehaviour
    {
        [SerializeField] public float angleOffset = -90;

        protected override void Apply()
        {
            var desiredRotation = QuaternionExt.LookRotation2D(DirectionToPlayer, angleOffset);
            var nextRotation = Quaternion.Slerp(Self.rotation, desiredRotation, Stats.rotationSpeed); 
            Self.rotation = nextRotation;
        }

        private Vector2 DirectionToPlayer 
            => (Player.position - Self.position).normalized;
    }
}