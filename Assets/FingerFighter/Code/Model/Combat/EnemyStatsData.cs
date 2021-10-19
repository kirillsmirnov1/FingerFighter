using System;
using UnityEngine;

namespace FingerFighter.Model.Combat
{
    [CreateAssetMenu(menuName = "Data/EnemyStats", fileName = "EnemyStats", order = 0)]
    public partial class EnemyStatsData : ScriptableObject
    {
        [SerializeField] private EnemyStats[] stats;

        public EnemyStats GetData(string tag)
        {
            for (int i = 0; i < stats.Length; i++)
            {
                if (stats[i].tag.Equals(tag)) return stats[i];
            }
            throw new ArgumentOutOfRangeException();
        }
        
        public bool HasEnemy(string tag)
        {
            for (int i = 0; i < stats.Length; i++)
            {
                if (stats[i].tag.Equals(tag)) return true;
            }
            return false;
        }
    }
}