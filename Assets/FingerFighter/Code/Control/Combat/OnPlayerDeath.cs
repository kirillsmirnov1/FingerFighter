using FingerFighter.Model.Combat;
using FingerFighter.View;
using UnityEngine;

namespace FingerFighter.Control.Combat
{
    public class OnPlayerDeath : MonoBehaviour
    {
        [Header("Data")] 
        [SerializeField] private HighScoreProcessor highScoreProcessor;
        
        [Header("View")] 
        [SerializeField] private OnPlayerDeathView playerDeathView;
        [SerializeField] private GameObject overlayScore;
        
        private void Awake() => PlayerStatus.OnDeath += OnDeath;
        private void OnDestroy() => PlayerStatus.OnDeath -= OnDeath;
        private void OnDeath()
        {
            overlayScore.SetActive(false);
            highScoreProcessor.Process();
            playerDeathView.Show();
        }
    }
}