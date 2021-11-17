using UnityEngine;

namespace FingerFighter.Control.Combat.Flow
{
    public partial class RunnerFlow
    {
        private class Room : State
        {
            private float _durationLeft;

            public Room(RunnerFlow flow) : base(flow) 
                => _durationLeft = Flow.roomDuration;

            public override void Enter()
            {
                var formation = Flow.NextFormation;
                Flow.spawn.Spawn(formation);
                RoomEntered($"{Flow._currentPack} : {formation.id}");
            }

            public override void Update()
            {
                _durationLeft -= Time.deltaTime;
                if (_durationLeft <= 0)
                {
                    Flow.NextRoom();
                }
            }

            public override void NoEnemiesLeft() 
                => Flow.NextRoom();
        }
    }
}