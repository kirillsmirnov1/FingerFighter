using UnityEngine;

namespace FingerFighter.Control.Enemies.Behaviour
{
    [CreateAssetMenu(menuName = "EnemyBehaviour/MoveForward", fileName = "MoveForward", order = 0)]
    public class MoveForward : AEnemyBehaviour
    {
        public override void Apply(EnemyBehaviourMachine enemy)
        {
            var movement = enemy.self.up * enemy.movementSpeed;
            enemy.rb.AddForce(movement, ForceMode2D.Force);
        }
    }
}