using FingerFighter.Model.Combat.Flow;
using UnityEngine;

namespace FingerFighter.Control.Combat.Flow
{
    public class RunnerFlowProxy : MonoBehaviour
    {
        [SerializeField] private RunnerFlowContainerVariable flowContainerVariable;

        private RunnerFlowContainer _runnerFlowContainer;
        private ARunnerFlow _runnerFlow;
        
        private void Awake()
        {
            _runnerFlowContainer = flowContainerVariable.Value;
            _runnerFlow = _runnerFlowContainer.runnerFlow;
            
            _runnerFlow.Init(this);
            _runnerFlow.OnAwake();
        }

        private void OnDestroy() 
            => _runnerFlow.OnDestroyed();

        private void OnEnable() 
            => _runnerFlow.OnEnabled();

        private void Update() 
            => _runnerFlow.OnUpdate();
    }
}