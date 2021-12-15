using FingerFighter.Control.Common.Combat.Status;
using FingerFighter.View.Common;
using UnityEngine;
using UnityUtils.Events;
using UnityUtils.Variables;

namespace FingerFighter.Control.Runner.Flow.Revive
{
    public class PaidRevive : MonoBehaviour
    {
        [SerializeField] private ulong baseReviveCost = 500;
        [SerializeField] private ULongVariable reviveCostVariable;
        [SerializeField] private ULongVariable coinBalance;
        [SerializeField] private GameEvent paidReviveRequest;
        [SerializeField] private GameEvent reward;
        
        private uint _costMultiplier;

        private void Awake()
        {
            _costMultiplier = 0;
            PlayerStatus.OnAlive += IterateReviveCostMultiplier;
            paidReviveRequest.RegisterAction(TryPaidRevive);
        }

        private void OnDestroy()
        {
            PlayerStatus.OnAlive -= IterateReviveCostMultiplier;
            paidReviveRequest.UnregisterAction(TryPaidRevive);
        }

        private void IterateReviveCostMultiplier()
        {
            _costMultiplier++;
            reviveCostVariable.Value = baseReviveCost * _costMultiplier;
        }

        private void TryPaidRevive()
        {
            if (coinBalance.Value >= reviveCostVariable.Value)
            {
                coinBalance.Value -= reviveCostVariable.Value;
                reward.Raise();
            }
            else
            {
                UiNotifications.Show("Don't have enough coins to revive.");
            }
        }
    }
}