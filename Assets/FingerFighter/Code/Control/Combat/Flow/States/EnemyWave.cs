namespace FingerFighter.Control.Combat.Flow
{
    public class EnemyWave : RunnerFlowState
    {
        public EnemyWave(ARunnerFlow flow) : base(flow) { }

        public override void Enter()
        {
            var formation = Flow.NextFormation;
            Flow.Spawn.Spawn(formation);
            WaveChanged($"{Flow.currentPack} : {formation.id}");
        }

        public override void Update() { }

        public override void NoEnemiesLeft()
            => Flow.GoToNextWave();
    }
}