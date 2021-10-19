using System;
using FingerFighter.Model.Combat;
using UnityEngine;

namespace FingerFighter.Model.EnemyFormations
{
    [Serializable]
    public struct EnemyFormationEntry
    {
        public EnemyStats enemy;
        public Vector2 pos;
    }
}