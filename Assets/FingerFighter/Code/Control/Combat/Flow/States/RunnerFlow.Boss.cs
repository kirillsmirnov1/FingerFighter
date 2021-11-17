namespace FingerFighter.Control.Combat.Flow
{
    public partial class RunnerFlow
    {
        private class Boss : State
        {
            public Boss(RunnerFlow flow) : base(flow) { }
            public override void Update() { }

            public override void Enter()
            {
                var formation = Flow.NextFormation;
                Flow.spawn.Spawn(formation);
                RoomEntered($"{Flow._currentPack} : {formation.id}");
            }

            public override void NoEnemiesLeft() 
                => Flow.NextRoom();
        }
    }
}