using FingerFighter.Control.Runner.Flow;
using UnityEngine;

namespace FingerFighter.Model.Runner.Flow
{
    [CreateAssetMenu(menuName = "Model/Flow/RunnerFlowContainer", fileName = "RunnerFlowContainer", order = 0)]
    public class RunnerFlowContainer : ScriptableObject
    {
        [SerializeField] public ARunnerFlow runnerFlow;
    }
}