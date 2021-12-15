using UnityEngine;

namespace FingerFighter.Model.Common.Combat.Damage.HitDataProvider
{
    public abstract class AHitDataProvider : MonoBehaviour 
    {
        public abstract HitData HitData { get; }
    }
}