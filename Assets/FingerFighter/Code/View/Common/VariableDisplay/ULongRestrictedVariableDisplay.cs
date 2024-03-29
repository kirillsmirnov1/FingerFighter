﻿using UnityUtils.Variables.Display;

namespace FingerFighter.View.Common.VariableDisplay
{
    public class ULongRestrictedVariableDisplay : ULongVariableDisplay
    {
        protected override void SetText(ulong value)
        {
            Text.gameObject.SetActive(value > 0);
            base.SetText(value);
        }
    }
}