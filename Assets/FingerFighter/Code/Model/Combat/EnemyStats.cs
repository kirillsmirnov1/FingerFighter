using System;

namespace FingerFighter.Model.Combat
{
    [Serializable]
    public class EnemyStats
    {
        public string tag;
        public float health;
        public float movementSpeed;
        public float rotationSpeed;
        public float collisionDamage;
    }
}