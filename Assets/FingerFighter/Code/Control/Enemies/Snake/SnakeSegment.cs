using FingerFighter.Model.Util;
using UnityEngine;

namespace FingerFighter.Control.Enemies.Snake
{
    public class SnakeSegment : MonoBehaviour
    {
        [SerializeField] private SnakeHead head;
        [SerializeField] private Rigidbody2D rb;
        
        [HideInInspector] public SnakeSegment previous;
        [HideInInspector] public SnakeSegment next;
        
        private Transform _target;
        private float _movementSpeed;
        private float _rotationSpeed;
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
            _target = IsHead ? head.Target : previous.transform;
        }

        private void SetParams()
        {
            var mod = IsHead ? 1f : 2f;
            _movementSpeed = mod * head.MovementSpeed;
            _rotationSpeed = head.RotationSpeed;
        }


        public void FixedUpdate()
        {
            RotateToTarget();
            MoveForward();
        }

        private void RotateToTarget()
        {
            // TODO refactor behaviour machine 
            Vector2 toTarget = _target.position - transform.position;
            var desiredRotation = QuaternionExt.LookRotation2D(toTarget, -90f);
            var nextRotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed);
            rb.MoveRotation(nextRotation);
        }

        private void MoveForward()
        {
            var movement = transform.up * _movementSpeed;
            rb.AddForce(movement, ForceMode2D.Force);
        }
    }
}