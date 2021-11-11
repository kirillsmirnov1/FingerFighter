using UnityUtils.Variables.Display;

namespace FingerFighter.View
{
    public class HighScoreDisplay : LongVariableDisplay
    {
        protected override void SetText(long value)
        {
            Text.gameObject.SetActive(value > 0);
            base.SetText(value);
        }
    }
}