using UnityEngine;

namespace FingerFighter.Model.Combat
{
    public class EnemyStats : ScriptableObject
    {
        public string tag;
        public float health;
        public float movementSpeed;
        public float rotationSpeed;
        public float collisionDamage;

        private void OnValidate()
        {
            name = tag;
        }
    }
}