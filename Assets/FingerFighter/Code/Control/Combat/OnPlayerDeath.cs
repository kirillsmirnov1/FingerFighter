using UnityEngine;
using UnityEngine.Events;
using UnityUtils.Variables;

namespace FingerFighter.Control.Combat
{
    public class OnPlayerDeath : MonoBehaviour
    {
        [SerializeField] private UnityEvent actions;
        [SerializeField] private FloatVariable runScore;
        [SerializeField] private LongVariable runHighScore;
        
        private void Awake() => PlayerStatus.OnDeath += OnDeath;
        private void OnDestroy() => PlayerStatus.OnDeath -= OnDeath;
        private void OnDeath()
        {
            var newHighScore = runScore.Value > runHighScore.Value;
            if (newHighScore) runHighScore.Value = (long) runScore.Value;
            // TODO hide overlay score 
            // TODO if high score, show prompt 

            // TODO show view 
            actions.Invoke();
        }
    }
}