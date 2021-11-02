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
        public bool IsHead { get; private set; }

        private void OnEnable()
        {
            Init();
        }

        private void OnDisable()
        {
            IsHead = false;
            OnSegmentLoss();
        }

        private void OnSegmentLoss()
        {
            FixSegmentsConnection();
        }

        private void FixSegmentsConnection()
        {
            if (previous != null)
            {
                previous.next = next;
                previous.Init();
            }

            if (next != null)
            {
                next.previous = previous;
                next.Init();
            }
        }

        private void Init()
        {
            SetTarget();
            SetParams();
        }

        private void SetTarget()
        {
            IsHead = previous == null;
            _target = IsHead ? head.Player : previous.transform;
        }

        private void SetParams()
        {
            _movementSpeed = head.MovementSpeed;
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