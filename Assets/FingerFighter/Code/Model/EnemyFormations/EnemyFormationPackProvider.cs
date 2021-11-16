using System.Collections.Generic;
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

        public IEnumerable<EnemyFormation> NextPack()
        {
            var pack = PickAPack();
            var formations = PickFormations(pack.Formations);
            return formations
                .Append(BossEntry(pack));
        }

        private static EnemyFormation BossEntry(EnemyFormationPack pack) 
            => new EnemyFormation {
                entries = new [] {
                    new EnemyFormationEntry {
                        enemy = pack.Boss,
                        pos = Vector2.zero,
                    }}};

        private IEnumerable<EnemyFormation> PickFormations(EnemyFormation[] formations)
        {
            var indexes = Enumerable
                .Range(0, formations.Length)
                .Shuffle()
                .Take(formationsPerRun)
                .OrderBy(x => x);
            
            foreach (var index in indexes)
            {
                yield return formations[index];
            }
        }

        private EnemyFormationPack PickAPack() 
            => packs.Value.Shuffle().First();
    }
}