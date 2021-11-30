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
        public float moveForwardLimit;
        
        [Header("Rewards")]
        public int points;
        public int coins;

        [Header("Formation Editor")] 
        public Color gizmoColor;
        public Sprite gizmoIcon;
        
        private void OnValidate()
        {
            name = tag;
        }
    }
}