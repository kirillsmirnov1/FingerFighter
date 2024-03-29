﻿using FingerFighter.Control.Runner.Flow;
using TMPro;
using UnityEngine;

namespace FingerFighter.View.Common.TextColorAnim
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class RunnerWaveInfoDisplay : TmpTextFade
    {
        private void Awake() 
            => RunnerFlowState.OnWaveStart += OnWaveStart;

        private void OnDestroy() 
            => RunnerFlowState.OnWaveStart -= OnWaveStart;

        private void OnWaveStart(string roomName)
        {
            SetText(roomName);
            ResetDurationTimer();
        }
    }
}