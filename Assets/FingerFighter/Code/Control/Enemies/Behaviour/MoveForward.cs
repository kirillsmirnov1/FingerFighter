using UnityEngine;

namespace FingerFighter.Control.Enemies.Behaviour
{
    public class MoveForward : AEnemyBehaviour
    {
        protected override void Apply()
        {
            var movement = Self.up * Stats.movementSpeed;
            rb.AddForce(movement, ForceMode2D.Force);
        }
    }
}