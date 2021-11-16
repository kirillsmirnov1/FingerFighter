using System;

namespace FingerFighter.Model.EnemyFormations
{
    [Serializable]
    public struct EnemyFormation
    {
        public string id;
        public EnemyFormationEntry[] entries;
    }
}