namespace FingerFighter.Control.Combat.Flow
{
    public class BossWave : RunnerFlowState
    {
        public BossWave(ARunnerFlow flow) : base(flow)
        {
        }

        public override void Update()
        {
        }

        public override void Enter()
        {
            var formation = Flow.NextFormation;
            Flow.spawn.Spawn(formation);
            WaveChanged($"{Flow.currentPack} : {formation.id}");
        }

        public override void NoEnemiesLeft()
            => Flow.GoToNextWave();
    }
}