using FingerFighter.Control.Combat;
using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Model.Combat
{
    public class PointsCounter : MonoBehaviour
    {
        [SerializeField] private FloatVariable pointsCounter;
        [SerializeField] private EnemyStatsList enemyData;
        
        private void Awake()
        {
            pointsCounter.Value = 0;
            EnemyStatus.OnDeath += OnEnemyDeath;
        }

        private void OnDestroy() => EnemyStatus.OnDeath -= OnEnemyDeath;

        private void OnEnemyDeath(string enemyTag, bool isSegment, Vector2 pos)
        {
            if(isSegment) return;
            var points = enemyData[enemyTag].health; // IMPR might change points logic later 
            pointsCounter.Value += points;
        }
    }
}