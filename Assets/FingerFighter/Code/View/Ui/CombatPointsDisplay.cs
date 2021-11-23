using UnityEngine;
using UnityUtils.Variables.Display;

namespace FingerFighter.View.Ui
{
    public class CombatPointsDisplay : FloatVariableDisplay
    {
        [SerializeField] private bool showZero;
        
        protected override void SetText(float value)
        {
            Text.gameObject.SetActive(value >= 1f || showZero);
            Text.text = $"{prefix}{value:0}{postfix}";
        }
    }
}