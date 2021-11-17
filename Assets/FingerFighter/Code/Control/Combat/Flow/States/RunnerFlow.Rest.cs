using UnityEngine;

namespace FingerFighter.Control.Combat.Flow
{
    public partial class RunnerFlow
    {
        private class Rest : State
        {
            private float _durationLeft;
            
            public Rest(RunnerFlow flow) : base(flow) { }
            public override void NoEnemiesLeft() { }
            
            public override void Enter()
            {
                _durationLeft = Flow.restDuration;
                RoomEntered("REST");
            }

            public override void Update()
            {
                _durationLeft -= Time.deltaTime;
                if (_durationLeft <= 0)
                {
                    Flow.NextRoom();
                }
            }
        }
    }
}