using FingerFighter.Control.Input.Handles;
using UnityEngine;

namespace FingerFighter.Model.Combat.Damage.HitDataProvider
{
    public class PlayerHitDataProvider : AHitDataProvider
    {
        [SerializeField] private HandleSpeed handleSpeed;
        
        public override HitData HitData => new HitData()
        {
            Force = handleSpeed.Speed,
            Direction = handleSpeed.Direction
        };
    }
}