﻿using FingerFighter.Control.Combat.Status;
using FingerFighter.Model.Combat;
using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Control.Balance
{
    public class RunAttemptCoinGainCounter : MonoBehaviour
    {
        [SerializeField] private EnemyStatsList stats;
        [SerializeField] private ULongVariable runAttemptGain;
        
        private void Awake()
        {
            EnemyStatus.OnDeath += GiveCoins;
            PlayerStatus.OnAlive += DropGain;
        }

        private void OnDestroy()
        {
            EnemyStatus.OnDeath -= GiveCoins;
            PlayerStatus.OnAlive -= DropGain;
        }

        private void DropGain()
        {
            runAttemptGain.Value = 0;
        }

        private void GiveCoins(EnemyDeathData edd)
        {
            runAttemptGain.Value += (ulong) stats[edd.Tag].coins;
        }
    }
}