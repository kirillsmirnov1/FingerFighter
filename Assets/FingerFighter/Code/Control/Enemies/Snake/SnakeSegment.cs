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
        
        private SnakeSegmentState _state;

        private void OnEnable()
        {
            SetState();
        }

        private void OnDisable()
        {
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
                previous.SetState();
            }

            if (next != null)
            {
                next.previous = previous;
                next.SetState();
            }
        }

        private void SetState()
        {
            _state = previous == null ? (SnakeSegmentState) new Head(this) : new Segment(this);
        }

        private void FixedUpdate()
        {
            _state.FixedUpdate();
        }

        private abstract class SnakeSegmentState
        {
            protected readonly SnakeSegment Self;
            protected SnakeSegmentState(SnakeSegment segment) => Self = segment;

            protected abstract Transform Target { get; }
            
            public void FixedUpdate()
            {
                RotateToTarget();
                MoveForward();
            }

            private void RotateToTarget()
            {
                // TODO cache data from head 
                // TODO refactor behaviour machine 
                Vector2 toTarget = Target.position - Self.transform.position;
                var desiredRotation = QuaternionExt.LookRotation2DAngle(toTarget, -90f);
                var nextRotation = Mathf.Lerp(Self.rb.rotation, desiredRotation, Self.head.RotationSpeed);
                Self.rb.rotation = nextRotation;
            }

            private void MoveForward()
            {
                var movement = Self.transform.up * Self.head.MovementSpeed;
                Self.rb.AddForce(movement, ForceMode2D.Force);
            }
        }

        private class Head : SnakeSegmentState
        {
            public Head(SnakeSegment segment) : base(segment) { }
            protected override Transform Target => Self.head.Player;
        }

        private class Segment : SnakeSegmentState
        {
            public Segment(SnakeSegment segment) : base(segment) { }
            protected override Transform Target => Self.previous.transform;
        }
    }
}