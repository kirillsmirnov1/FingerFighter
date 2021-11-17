using UnityEngine;

namespace FingerFighter.Control.Combat.Flow
{
    public partial class RunnerFlow
    {
        private class Rest : State
        {
            private float _durationLeft;

            public Rest(RunnerFlow flow) : base(flow) 
                => _durationLeft = Flow.restDuration;

            public override void NoEnemiesLeft() { }
            
            public override void Enter()
            {
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