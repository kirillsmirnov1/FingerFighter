using System;

namespace FingerFighter.Control.Runner.Flow
{
    public abstract class RunnerFlowState
    {
        public static event Action<string> OnWaveStart;

        protected static void WaveChanged(string roomName)
            => OnWaveStart?.Invoke(roomName);

        protected readonly ARunnerFlow Flow;

        protected RunnerFlowState(ARunnerFlow flow) => Flow = flow;
        public abstract void Enter();
        public abstract void Update();
        public abstract void NoEnemiesLeft();
    }
}