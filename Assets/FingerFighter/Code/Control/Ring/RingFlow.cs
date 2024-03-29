﻿using FingerFighter.Control.Common.Combat.Health;
using FingerFighter.Model.LevelMaps;
using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Control.Ring
{
    public class RingFlow : MonoBehaviour
    {
        [SerializeField] private LevelMapVariable levelMapVariable;
        [SerializeField] private FloatVariable levelScore;
        [SerializeField] private PlayerHealthHelper playerHealthHelper;
        
        private void Awake()
        {
            levelMapVariable.Value = null; // Because ring loaded only when level is over
            levelScore.Value = 0;
            playerHealthHelper.Revive();
        }
    }
}