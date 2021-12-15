using UnityEngine;

namespace FingerFighter.Control.Runner.Flow
{
    public class EnemyWaveLimitedTime : EnemyWave
    {
        private float _durationLeft;

        public EnemyWaveLimitedTime(ARunnerFlow flow) : base(flow)
            => _durationLeft = Flow.roomDuration;

        public override void Update()
        {
            _durationLeft -= Time.deltaTime * Flow.combatTimeScale;
            if (_durationLeft <= 0)
            {
                Flow.GoToNextWave();
            }
        }
    }
}