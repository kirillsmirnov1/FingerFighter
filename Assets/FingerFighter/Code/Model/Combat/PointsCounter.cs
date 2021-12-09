using FingerFighter.Control.Combat.Status;
using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Model.Combat
{
    public class PointsCounter : MonoBehaviour
    {
        [SerializeField] private FloatVariable pointsCounter;
        [SerializeField] private EnemyStatsList enemyData;
        
        private void Awake() => EnemyStatus.OnDeath += OnEnemyDeath;
        private void OnDestroy() => EnemyStatus.OnDeath -= OnEnemyDeath;

        private void OnEnemyDeath(EnemyDeathData enemyDeathData)
        {
            if(enemyDeathData.IsSegment) return;
            var points = enemyData[enemyDeathData.Tag].points; 
            pointsCounter.Value += points;
        }
    }
}