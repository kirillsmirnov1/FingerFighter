using FingerFighter.Model.Common.Combat;
using FingerFighter.Model.Common.Enemies;
using UnityEngine;

namespace FingerFighter.Control.Common.Enemies.Behaviour
{
    [RequireComponent(typeof(EnemyComponents))]
    public abstract class AEnemyBehaviour : MonoBehaviour
    {
        [SerializeField] private EnemyComponents components;

        protected virtual void OnValidate()
        {
            components ??= GetComponent<EnemyComponents>();
        }

        protected float CombatTimeScale => components.combatTimeScale.Value;
        protected EnemyStats Stats => components.stats;
        protected Rigidbody2D Rb => components.rb;

        protected Transform Self;
        protected Transform Target => components.Target;

        protected float MovementSpeed;
        
        protected virtual void Awake()
        {
            Self = transform;
            MovementSpeed = Stats.movementSpeed;
        }

        private void FixedUpdate() => Apply();

        protected abstract void Apply();

        public void OverrideMovementSpeed(float movementSpeed)
            => MovementSpeed = movementSpeed;
    }
}