using System;

namespace FingerFighter.Control.Combat.Flow
{
    public partial class RunnerFlow
    {
        public abstract class State
        {
            public static event Action<string> OnRoomEntered;
            protected static void RoomEntered(string roomName) 
                => OnRoomEntered?.Invoke(roomName);

            protected readonly RunnerFlow Flow;

            protected State(RunnerFlow flow) => Flow = flow;
            public abstract void Enter();
            public abstract void Update();
            public abstract void NoEnemiesLeft();
        }
    }
}