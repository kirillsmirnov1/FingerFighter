using FingerFighter.Utils;

namespace FingerFighter.Control.Enemies.Behaviour
{
    public class MoveForward : AEnemyBehaviour
    {
        protected override void Apply()
        {
            var acceleration = Self.up * MovementSpeed;
            rb.AddAcceleration(acceleration);
        }
    }
}