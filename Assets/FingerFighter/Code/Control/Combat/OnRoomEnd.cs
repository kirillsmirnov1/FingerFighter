using FingerFighter.Control.Combat.Flow;
using FingerFighter.Control.Combat.Status;
using FingerFighter.Model.Combat;
using FingerFighter.View.Display;
using UnityEngine;

namespace FingerFighter.Control.Combat
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