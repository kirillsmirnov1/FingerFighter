using UnityEngine;

namespace FingerFighter.Model.Combat.Damage
{
    public struct HitData
    {
        public float Force;
        public Vector2 Direction;
        public Vector2 Position;
        public Affiliation Affected;
    }
}