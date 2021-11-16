using System.Linq;
using UnityEngine;
using UnityUtils;

namespace FingerFighter.Model.EnemyFormations
{
    [CreateAssetMenu(menuName = "Model/EnemyFormationPackProvider", fileName = "EnemyFormationPackProvider", order = 0)]
    public class EnemyFormationPackProvider : ScriptableObject
    {
        [SerializeField] private EnemyFormationPackArray packs;
        [SerializeField] private int formationsPerRun = 10;

        public EnemyFormation[] NextPack()
        {
            var pack = PickAPack();
            // TODO pick formations
            // TODO add boss 
            return pack.Formations; 
        }

        private EnemyFormationPack PickAPack() 
            => packs.Value.Shuffle().First();
    }
}