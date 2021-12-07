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

        public PackToSpawn NextRandomPack()
        {
            var pack = PickRandomPack();
            var formations = PickRandomFormations(pack.Formations);
            return new PackToSpawn
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

        private IEnumerable<EnemyFormation> PickRandomFormations(EnemyFormation[] formations)
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

        private EnemyFormationPack PickRandomPack() 
            => packs.Value.Shuffle().First();

        public IEnumerable<EnemyFormation> GetFormations(string packId, List<int> formationIds)
        {
            var pack = GetPackById(packId);
            for (var i = 0; i < formationIds.Count; i++)
            {
                yield return pack.Formations[formationIds[i]];
            }
        }

        public IEnumerable<EnemyFormation> GetBossFormation(string packId)
        {
            var pack = GetPackById(packId);
            yield return BossEntry(pack);
        }

        private EnemyFormationPack GetPackById(string packId) 
            => packs.Value.First(pack => pack.Id.Equals(packId));
    }
}