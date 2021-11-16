using System.Collections.Generic;

namespace FingerFighter.Model.EnemyFormations
{
    public class PackToSpawn
    {
        public string ID;
        public IEnumerable<EnemyFormation> Formations;
    }
}