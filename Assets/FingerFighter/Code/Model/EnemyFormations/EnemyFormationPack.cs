using UnityEngine;

namespace FingerFighter.Model.EnemyFormations
{
    [CreateAssetMenu(menuName = "Data/EnemyFormationPack", fileName = "EnemyFormationPack", order = 0)]
    public class EnemyFormationPack : ScriptableObject
    {
        public EnemyFormation[] formations;
    }
}