using FingerFighter.View;
using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Control.Combat
{
    public class OnPlayerDeath : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private FloatVariable runScore;
        [SerializeField] private LongVariable runHighScore;
        
        [Header("View")] 
        [SerializeField] private OnPlayerDeathView playerDeathView;
        [SerializeField] private GameObject overlayScore;
        
        private void Awake() => PlayerStatus.OnDeath += OnDeath;
        private void OnDestroy() => PlayerStatus.OnDeath -= OnDeath;
        private void OnDeath()
        {
            var newHighScore = runScore.Value > runHighScore.Value;
            if (newHighScore) runHighScore.Value = (long) runScore.Value;
        
            overlayScore.SetActive(false);
            playerDeathView.Show(newHighScore);
        }
    }
}