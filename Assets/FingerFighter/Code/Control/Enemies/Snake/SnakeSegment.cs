using UnityEngine;

namespace FingerFighter.Control.Enemies.Snake
{
    public class SnakeSegment : MonoBehaviour
    {
        [SerializeField] private SnakeBody body;
        
        [SerializeField] private SnakeSegment previous;
        [SerializeField] private SnakeSegment next;

        [SerializeField] private Rigidbody2D rb;
        
        // TODO change on death of another segment 
        private SnakeSegmentState _state;

        private void Awake()
        {
            transform.parent = null; // TODO find another way of freeing from hierarchy 
        }

        private void OnEnable()
        {
            SetState();
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
            protected readonly SnakeSegment Segment;
            public SnakeSegmentState(SnakeSegment segment) => Segment = segment;
            public abstract void FixedUpdate();
        }

        private class Head : SnakeSegmentState
        {
            public Head(SnakeSegment segment) : base(segment) { }

            public override void FixedUpdate()
            {
                Segment.transform.position = Segment.body.transform.position;
                Segment.transform.rotation = Segment.body.transform.rotation;
            }
        }

        private class Segment : SnakeSegmentState
        {
            public Segment(SnakeSegment segment) : base(segment) { }

            public override void FixedUpdate()
            {
                var targetPosition = Vector2.Lerp(Segment.rb.position, Segment.previous.rb.position, 0.05f);
                var toTarget = targetPosition - Segment.rb.position;
                Vector3 moveVector = Vector2.ClampMagnitude(toTarget, .05f);
                Segment.transform.position += moveVector; 
                // TODO rotation 
            }
        }
    }
}