using System.Linq;
using FingerFighter.Control.Combat.Health;
using UnityEngine;

namespace FingerFighter.Control.Conditions.Flags
{
    /// <summary>
    /// On while all stageHealths are on 
    /// </summary>
    public class StageHealthFlag : AFlag
    {
        [SerializeField] private AHealth[] stageHealths;

        private void OnEnable()
        {
            CheckStageHealthsOnDepletion();
            for (int i = 0; i < stageHealths.Length; i++)
            {
                stageHealths[i].onNoHealth += CheckStageHealthsOnDepletion;
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < stageHealths.Length; i++)
            {
                stageHealths[i].onNoHealth -= CheckStageHealthsOnDepletion;
            }
        }

        private void CheckStageHealthsOnDepletion()
        {
            On = stageHealths.Any(h => !h.NoHealth);
        }
        
    }
}