using FingerFighter.Model.Combat;
using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Control.Enemies.Behaviour
{
    public abstract class AEnemyBehaviour : MonoBehaviour
    {
        [SerializeField] private CombatEntityId id;
        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected TransformVariable player;

        protected EnemyStats Stats => id.EnemyStats;

        protected Transform Self;
        protected Transform Target;

        protected float MovementSpeed;
        
        protected virtual void Awake()
        {
            Self = transform;
            Target = player.Value;
            MovementSpeed = Stats.movementSpeed;
        }

        private void OnValidate()
        {
            id ??= GetComponent<CombatEntityId>();
            rb ??= GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() => Apply();

        protected abstract void Apply();

        public void OverrideTarget(Transform target)
            => Target = target;

        public void OverrideMovementSpeed(float movementSpeed)
            => MovementSpeed = movementSpeed;
    }
}