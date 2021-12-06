using UnityEngine;

namespace FingerFighter.Control.Combat.Flow
{
    public class EnemyWaveLimitedTime : RunnerFlowState // TODO EnemyWave (unlimitedTime)
    {
        private float _durationLeft;

        public EnemyWaveLimitedTime(ARunnerFlow flow) : base(flow)
            => _durationLeft = Flow.roomDuration;

        public override void Enter()
        {
            var formation = Flow.NextFormation;
            Flow.spawn.Spawn(formation);
            WaveChanged($"{Flow.currentPack} : {formation.id}");
        }

        public override void Update()
        {
            _durationLeft -= Time.deltaTime * Flow.combatTimeScale;
            if (_durationLeft <= 0)
            {
                Flow.GoToNextWave();
            }
        }

        public override void NoEnemiesLeft()
            => Flow.GoToNextWave();
    }
}