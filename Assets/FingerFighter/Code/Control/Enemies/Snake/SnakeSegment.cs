using FingerFighter.Model.Util;
using UnityEngine;

namespace FingerFighter.Control.Enemies.Snake
{
    public class SnakeSegment : MonoBehaviour
    {
        [SerializeField] private SnakeHead head;
        
        [SerializeField] private SnakeSegment previous;
        [SerializeField] private SnakeSegment next;

        [SerializeField] private Rigidbody2D rb;
        
        private Transform _target;
        public bool IsHead { get; private set; }

        private void OnEnable()
        {
            SetTarget();
        }

        private void OnDisable()
        {
            IsHead = false;
            OnSegmentLoss();
        }

        private void OnSegmentLoss()
        {
            FixSegmentsConnection();
            // TODO notify body 
        }

        private void FixSegmentsConnection()
        {
            if (previous != null)
            {
                previous.next = next;
                previous.SetTarget();
            }

            if (next != null)
            {
                next.previous = previous;
                next.SetTarget();
            }
        }

        private void SetTarget()
        {
            IsHead = previous == null;
            _target = IsHead ? head.Player : previous.transform;
        }


        public void FixedUpdate()
        {
            RotateToTarget();
            MoveForward();
        }

        private void RotateToTarget()
        {
            // TODO cache data from head 
            // TODO refactor behaviour machine 
            Vector2 toTarget = _target.position - transform.position;
            var desiredRotation = QuaternionExt.LookRotation2D(toTarget, -90f);
            var nextRotation = Quaternion.Slerp(transform.rotation, desiredRotation, head.RotationSpeed);
            rb.MoveRotation(nextRotation);
        }

        private void MoveForward()
        {
            var movement = transform.up * head.MovementSpeed;
            rb.AddForce(movement, ForceMode2D.Force);
        }
    }
}