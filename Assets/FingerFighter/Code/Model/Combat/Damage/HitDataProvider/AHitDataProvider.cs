using UnityEngine;

namespace FingerFighter.Model.Combat.Damage.HitDataProvider
{
    public abstract class AHitDataProvider : MonoBehaviour 
    {
        public abstract HitData HitData { get; }
    }
}