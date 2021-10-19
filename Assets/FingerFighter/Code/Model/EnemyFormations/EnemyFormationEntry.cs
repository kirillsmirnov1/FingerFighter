using System;
using UnityEngine;

namespace FingerFighter.Model.EnemyFormations
{
    [Serializable]
    public struct EnemyFormationEntry
    {
        public string enemyType;
        public Vector2 pos;
    }
}