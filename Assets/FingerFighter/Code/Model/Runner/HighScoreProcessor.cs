using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Model.Runner
{
    [CreateAssetMenu(menuName = "Model/HighScoreProcessor", fileName = "HighScoreProcessor", order = 0)]
    public class HighScoreProcessor : ScriptableObject
    {
        [SerializeField] private FloatVariable runScore;
        [SerializeField] private LongVariable runHighScore;
        [SerializeField] private BoolVariable newHighScore;

        public void Process()
        {
            newHighScore.Value = runScore.Value > runHighScore.Value;
            if (newHighScore) runHighScore.Value = (long) runScore.Value;
        }
    }
}