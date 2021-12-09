using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Model.EnemyFormations
{
    [CreateAssetMenu(menuName = "Data/EnemyFormationPackArray", fileName = "EnemyFormationPack", order = 0)]
    public class EnemyFormationPackArray : XArrayVariable<EnemyFormationPack>
    {
        public EnemyFormationPack Get(string id)
        {
            var index = GetIndex(id);

            if (index != -1)
            {
                return Value[index];
            }

            Debug.LogWarning($"Couldn't get «{id}» level, returning {Value[0].Id}");
            return Value[0];
        }

        private int GetIndex(string id)
        {
            var packs = Value;
            for (int i = 0; i < packs.Length; i++)
            {
                if (packs[i].Id.Equals(id))
                    return i;
            }
            return -1;
        }
    }
}