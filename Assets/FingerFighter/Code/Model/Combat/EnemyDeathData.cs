using UnityEngine;

namespace FingerFighter.Model.Combat
{
    public struct EnemyDeathData
    {
        public string Tag;
        public bool IsSegment;
        public bool IsProjectile;
        public Vector2 DeathPos;
    }
}