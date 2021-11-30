using UnityUtils.Variables.Display;

namespace FingerFighter.View.Ui
{
    public class LongRestrictedVariableDisplay : LongVariableDisplay
    {
        protected override void SetText(long value)
        {
            Text.gameObject.SetActive(value > 0);
            base.SetText(value);
        }
    }
}