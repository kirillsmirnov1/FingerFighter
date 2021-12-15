using UnityEngine;

namespace FingerFighter.Control.Common.Enemies.Behaviour.Movement
{
    public class MoveForwardLimited : MoveForward
    {
        protected override void Apply()
        {
            if(Vector2.Distance(Self.position, Target.position) < Stats.moveForwardLimit) return;
            base.Apply();
        }
    }
}