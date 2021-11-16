using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Model.EnemyFormations
{
    [CreateAssetMenu(menuName = "Data/EnemyFormationPackArray", fileName = "EnemyFormationPack", order = 0)]
    public class EnemyFormationPackArray : XArrayVariable<EnemyFormationPack>
    {
        
    }
}