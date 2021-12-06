using System.Collections.Generic;
using FingerFighter.Model.EnemyFormations;
using UnityEngine;
using UnityUtils.Attributes;
using UnityUtils.Extensions;

namespace FingerFighter.Control.Combat.Flow
{
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

        public override void GoToNextWave()
        {
            state = null;
            if (formations.Count > 1)
            {
                state = new EnemyWaveLimitedTime(this);
            }
            else if(formations.Count == 1)
            {
                state = new BossWave(this);
            }
            else
            {
                state = new Rest(this);
                UpdateFormationsQueue();
            }
            this.DelayAction(0f, () => state.Enter());
        }
    }
}