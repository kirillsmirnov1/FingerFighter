using UnityUtils.Variables.Display;

namespace FingerFighter.View.Ui
{
    public class CombatPointsDisplay : FloatVariableDisplay
    {
        protected override void SetText(float value)
        {
            Text.gameObject.SetActive(value >= 1f);
            Text.text = $"{prefix}{value:0}{postfix}";
        }
    }
}