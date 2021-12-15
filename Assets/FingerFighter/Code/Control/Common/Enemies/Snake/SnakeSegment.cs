using FingerFighter.Control.Common.Enemies.Behaviour.Movement;
using FingerFighter.Model.Common.Enemies;
using UnityEngine;

namespace FingerFighter.Control.Common.Enemies.Snake
{
    public class SnakeSegment : MonoBehaviour
    {
        [SerializeField] private SnakeHead head;
        [SerializeField] private MoveForward movementForward;
        [SerializeField] private EnemyComponents components;

        [HideInInspector] public SnakeSegment previous;
        [HideInInspector] public SnakeSegment next;
        
        public bool IsHead { get; protected set; }

        private void OnValidate()
        {
            components ??= GetComponent<EnemyComponents>();
        }

        protected virtual void OnEnable()
        {
            Init();
        }

        private void OnDisable()
        {
            IsHead = false;
            OnSegmentLoss();
        }

        protected void OnSegmentLoss()
        {
            FixSegmentsConnection();
        }

        private void FixSegmentsConnection()
        {
            if (previous != null && previous.gameObject.activeSelf)
            {
                previous.next = next;
                previous.Init();
            }

            if (next != null && next.gameObject.activeSelf)
            {
                next.previous = previous;
                next.Init();
            }
            
            head.UpdateHeadSegment();
        }

        private void Init()
        {
            SetTarget();
            SetParams();
        }

        protected virtual void SetTarget()
        {
            IsHead = previous == null;
            components.OverrideTarget(IsHead ? head.Target : previous.transform);
        }

        private void SetParams()
        {
            var mod = IsHead ? 1f : 2f;
            movementForward.OverrideMovementSpeed(mod * head.MovementSpeed);
        }
    }
}