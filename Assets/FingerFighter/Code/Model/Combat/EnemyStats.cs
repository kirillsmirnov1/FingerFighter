using UnityEngine;

namespace FingerFighter.Model.Combat
{
    public class EnemyStats : ScriptableObject
    {
        public string tag;
     
        [Header("Combat")]
        public float health;
        public float movementSpeed;
        public float rotationSpeed;
        public float collisionDamage;

        [Header("Formation Editor")] 
        public Color gizmoColor;
        
        private void OnValidate()
        {
            name = tag;
        }
    }
}