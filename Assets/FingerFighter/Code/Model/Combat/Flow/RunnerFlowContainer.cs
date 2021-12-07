using FingerFighter.Control.Combat.Flow;
using UnityEngine;

namespace FingerFighter.Model.Combat.Flow
{
    [CreateAssetMenu(menuName = "Model/Flow/RunnerFlowContainer", fileName = "RunnerFlowContainer", order = 0)]
    public class RunnerFlowContainer : ScriptableObject
    {
        [SerializeField] public ARunnerFlow runnerFlow;
    }
}