using UnityEngine;

namespace FingerFighter.Control.Enemies.Snake
{
    public class SnakeSegment : MonoBehaviour
    {
        [SerializeField] private SnakeBody body;
        
        [SerializeField] private SnakeSegment previous;
        [SerializeField] private SnakeSegment next;

        private SnakeSegmentState _state;
        
        private void OnEnable()
        {
            SetState();
        }

        private void SetState()
        {
            _state = previous == null ? (SnakeSegmentState) new Head() : new Segment();
        }

        private void FixedUpdate()
        {
            _state.FixedUpdate();
        }

        private abstract class SnakeSegmentState
        {
            public abstract void FixedUpdate();
        }

        private class Head : SnakeSegmentState
        {
            public override void FixedUpdate()
            {
                // TODO follow player 
            }
        }

        private class Segment : SnakeSegmentState
        {
            public override void FixedUpdate()
            {
                // TODO follow previous segment 
            }
        }
    }
}