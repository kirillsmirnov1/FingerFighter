using System.Collections.Generic;
using FingerFighter.Model.EnemyFormations;
using UnityEngine;
using UnityUtils.Attributes;

namespace FingerFighter.Control.Runner.Flow
{
    [CreateAssetMenu(menuName = "Model/Flow/InfiniteRunnerFlow", fileName = "InfiniteRunnerFlow", order = 0)]
    public class InfiniteRunnerFlow : ARunnerFlow  
    {
        [Separator("Infinite Runner Flow")]
        [Header("Params")]
        [SerializeField] public float restDuration = 25f;

        protected override void UpdateFormationsQueue()
        {
            var pack = enemyProvider.NextRandomPack();
            currentPack = pack.ID;
            formations = new Queue<EnemyFormation>(pack.Formations);
        }

        protected override void OnNoFormationsLeft()
        {
            state = new Rest(this);
            UpdateFormationsQueue();
        }
    }
}