using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Model.EnemyFormations
{
    [CreateAssetMenu(menuName = "Data/EnemyFormationPackArray", fileName = "EnemyFormationPack", order = 0)]
    public class EnemyFormationPackArray : XArrayVariable<EnemyFormationPack>
    {
        public EnemyFormationPack Get(string id)
        {
            foreach (var pack in Value)
            {
                if (pack.Id.Equals(id)) 
                    return pack;
            }
            Debug.LogWarning($"Couldn't get «{id}» level, returning {Value[0].Id}");
            return Value[0];
        }
    }
}