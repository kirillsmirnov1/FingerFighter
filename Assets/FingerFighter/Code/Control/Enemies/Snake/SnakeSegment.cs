using FingerFighter.Control.Enemies.Behaviour;
using UnityEngine;

namespace FingerFighter.Control.Enemies.Snake
{
    public class SnakeSegment : MonoBehaviour
    {
        [SerializeField] private SnakeHead head;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private RotateTowardsTarget rotation;
        
        [HideInInspector] public SnakeSegment previous;
        [HideInInspector] public SnakeSegment next;
        
        private float _movementSpeed;
        public bool IsHead { get; protected set; }

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
            rotation.OverrideTarget(IsHead ? head.Target : previous.transform);
        }

        private void SetParams()
        {
            var mod = IsHead ? 1f : 2f;
            _movementSpeed = mod * head.MovementSpeed;
        }


        public void FixedUpdate()
        {
            MoveForward();
        }

        private void MoveForward()
        {
            var movement = transform.up * _movementSpeed;
            rb.AddForce(movement, ForceMode2D.Force);
        }
    }
}