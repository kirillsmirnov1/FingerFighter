using UnityEngine;

namespace FingerFighter.Control.Enemies.Behaviour
{
    [CreateAssetMenu(menuName = "EnemyBehaviour/RotateTowardsPlayer", fileName = "RotateTowardsPlayer", order = 0)]
    public class RotateTowardsPlayer : AEnemyBehaviour
    {
        public override void Apply(EnemyBehaviourMachine enemy)
        {
            var dirToPlayer = enemy.directionToPlayer;
            var angle = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;
            var desiredRotation = Quaternion.Euler(0, 0, angle + enemy.angleOffset);
            var nextRotation = Quaternion.Slerp(enemy.transform.rotation, desiredRotation, enemy.Stats.rotationSpeed); 
            enemy.transform.rotation = nextRotation;
        }
    }
}