using FingerFighter.Control.Combat.Status;
using FingerFighter.Model.Combat;
using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Control.Balance
{
    public class GiveCoinsOnEnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyStatsList stats;
        
        [SerializeField] private ULongVariable coinBalance;

        private void Awake()
        {
            EnemyStatus.OnDeath += GiveCoins;
        }

        private void OnDestroy()
        {
            EnemyStatus.OnDeath -= GiveCoins;
        }

        private void GiveCoins(EnemyDeathData edd)
        {
            coinBalance.Value += (ulong) stats[edd.Tag].coins;
        }
    }
}