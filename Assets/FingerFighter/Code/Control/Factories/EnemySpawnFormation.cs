using FingerFighter.Model.EnemyFormations;
using UnityEngine;

namespace FingerFighter.Control.Factories
{
    public class EnemySpawnFormation : EnemySpawn
    {
        [SerializeField] private EnemyFormationPack pack;
        
        private int _i;
        
        protected override void Spawn()
        {
            var formation = NextFormation().entries;
            for (int i = 0; i < formation.Length; i++)
            {
                SpawnEnemy(formation[i].enemy.tag, formation[i].pos);
            }
        }

        private EnemyFormation NextFormation()
        {
            var formation = pack.Formations[_i];
            _i = (_i + 1) % pack.Formations.Length;
            return formation;
        }
    }
}