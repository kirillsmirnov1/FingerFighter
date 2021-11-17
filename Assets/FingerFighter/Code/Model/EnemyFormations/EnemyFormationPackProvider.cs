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

        public PackToSpawn NextPack()
        {
            var pack = PickAPack();
            var formations = PickFormations(pack.Formations);
            return new PackToSpawn()
            {
                ID = pack.Id,
                Formations = formations.Append(BossEntry(pack))
            };
        }

        private static EnemyFormation BossEntry(EnemyFormationPack pack) 
            => new EnemyFormation {
                id = pack.Boss.name.ToUpper(),
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
                .Take(Mathf.Min(formationsPerRun, formations.Length))
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