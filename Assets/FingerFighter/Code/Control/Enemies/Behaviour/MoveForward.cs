using UnityEngine;

namespace FingerFighter.Control.Enemies.Behaviour
{
    public class MoveForward : AEnemyBehaviour
    {
        protected override void Apply()
        {
            var movement = Self.up * MovementSpeed;
            rb.AddForce(movement, ForceMode2D.Force);
        }
    }
}