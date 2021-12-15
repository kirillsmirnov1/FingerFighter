using UnityEngine;

namespace FingerFighter.Control.Runner.Flow
{
    public class Rest : RunnerFlowState
    {
        private float _durationLeft;

        public Rest(InfiniteRunnerFlow flow) : base(flow)
            => _durationLeft = flow.restDuration;

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
                Flow.GoToNextWave();
            }
        }
    }
}