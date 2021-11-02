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
            public abstract void FixedUpdate();
        }

        private class Head : SnakeSegmentState
        {
            public Head(SnakeSegment segment) : base(segment) { }

            public override void FixedUpdate()
            {
                Self.rb.MovePosition(Self.head.transform.position);
                Self.rb.MoveRotation(Self.head.transform.rotation);
            }
        }

        private class Segment : SnakeSegmentState
        {
            public Segment(SnakeSegment segment) : base(segment) { }

            public override void FixedUpdate()
            {
                var targetPosition = Vector2.Lerp(Self.rb.position, Self.previous.rb.position, 0.05f);
                var toTarget = targetPosition - Self.rb.position;
                var moveVector = Vector2.ClampMagnitude(toTarget, .05f);
                Self.rb.MovePosition(Self.rb.position + moveVector);
                Self.rb.MoveRotation(QuaternionExt.LookRotation2DAngle(toTarget, -90f));
            }
        }
    }
}