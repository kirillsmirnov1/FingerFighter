using UnityUtils.Variables.Display;

namespace FingerFighter.View.Common.VariableDisplay
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