using UnityEngine;

namespace FingerFighter.Model.Combat
{
    public class EnemyStats : ScriptableObject
    {
        public string tag;
        public GameObject prefab;
     
        [Header("Combat")]
        public float health;
        public float movementSpeed;
        public float rotationSpeed;
        public float collisionDamage;

        [Header("Formation Editor")] 
        public Color gizmoColor;
        public Sprite gizmoIcon;
        
        private void OnValidate()
        {
            name = tag;
        }
    }
}