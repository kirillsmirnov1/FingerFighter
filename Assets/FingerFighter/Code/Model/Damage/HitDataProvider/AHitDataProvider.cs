using UnityEngine;

namespace FingerFighter.Model.Damage.HitDataProvider
{
    public abstract class AHitDataProvider : MonoBehaviour 
    {
        public abstract HitData HitData { get; }
    }
}