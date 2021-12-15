using FingerFighter.Control.Common.Combat.Status;
using FingerFighter.Model.Runner;
using FingerFighter.View.Runner;
using UnityEngine;

namespace FingerFighter.Control.Runner.Flow
{
    public class OnRoomEnd : MonoBehaviour
    {
        [Header("Data")] 
        [SerializeField] private HighScoreProcessor highScoreProcessor;
        
        [Header("View")] 
        [SerializeField] private OnRoomEndView playerDeathView;
        [SerializeField] private GameObject inGameOverlay;
        
        private void Awake()
        {
            PlayerStatus.OnDeath += OnDeath;
            ARunnerFlow.OnPlayerWon += OnPlayerWin;
        }

        private void OnDestroy()
        {
            PlayerStatus.OnDeath -= OnDeath;
            ARunnerFlow.OnPlayerWon -= OnPlayerWin;
        }

        private void OnPlayerWin()
        {
            inGameOverlay.SetActive(false);
            highScoreProcessor.Process();
            playerDeathView.ShowOnWin();
        }

        private void OnDeath()
        {
            inGameOverlay.SetActive(false);
            highScoreProcessor.Process();
            playerDeathView.ShowOnDeath();
        }
    }
}