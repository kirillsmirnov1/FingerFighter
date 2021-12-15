using UnityEngine;
using UnityUtils.Variables;
using UnityUtils.Variables.Display;

namespace FingerFighter.View.Common.VariableDisplay
{
    public class FloatVariablePercentDisplay : FloatVariableDisplay
    {
        [SerializeField] private FloatVariable divider;
        protected override void SetText(float value)
        {
            var percent = (int)(100f * value / divider);
            Text.text = $"{prefix}{percent}%{postfix}";
        }
    }
}
