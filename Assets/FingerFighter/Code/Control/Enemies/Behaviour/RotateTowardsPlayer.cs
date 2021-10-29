using FingerFighter.Model.Util;
using UnityEngine;

namespace FingerFighter.Control.Enemies.Behaviour
{
    [CreateAssetMenu(menuName = "EnemyBehaviour/RotateTowardsPlayer", fileName = "RotateTowardsPlayer", order = 0)]
    public class RotateTowardsPlayer : AEnemyBehaviour
    {
        public override void Apply(EnemyBehaviourMachine enemy)
        {
            var desiredRotation = QuaternionExt.LookRotation2D(enemy.directionToPlayer, enemy.angleOffset);
            var nextRotation = Quaternion.Slerp(enemy.transform.rotation, desiredRotation, enemy.Stats.rotationSpeed); 
            enemy.transform.rotation = nextRotation;
        }
    }
}