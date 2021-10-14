using UnityEngine;

namespace FingerFighter.Model.Damage.HitDataProvider
{
    public class ConstantHitForce : AHitDataProvider
    {
        [SerializeField] private float force = 5;
        
        public override HitData HitData => new HitData
        {
            Force = force
        };
    }
}