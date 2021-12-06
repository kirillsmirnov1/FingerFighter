using UnityEngine;

namespace FingerFighter.Control.Combat.Flow
{
    public class Rest : RunnerFlowState
    {
        private float _durationLeft;

        public Rest(InfiniteRunnerFlow flow) : base(flow)
            => _durationLeft = Flow.restDuration;

        public override void NoEnemiesLeft()
        {
        }

        public override void Enter()
        {
            WaveChanged("REST");
        }

        public override void Update()
        {
            _durationLeft -= Time.deltaTime;
            if (_durationLeft <= 0)
            {
                Flow.NextWave();
            }
        }
    }
}