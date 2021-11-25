using FingerFighter.Utils;

namespace FingerFighter.Control.Enemies.Behaviour.Movement
{
    public class MoveForward : AEnemyBehaviour
    {
        protected override void Apply()
        {
            var acceleration = Self.up * (MovementSpeed * CombatTimeScale);
            Rb.AddAcceleration(acceleration);
        }
    }
}