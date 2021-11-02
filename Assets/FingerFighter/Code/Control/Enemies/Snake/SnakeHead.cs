using FingerFighter.Model.Combat;
using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Control.Enemies.Snake
{
    public class SnakeHead : MonoBehaviour
    {
        [SerializeField] private TransformVariable player;
        [SerializeField] private CombatEntityId id;

        private SnakeSegment[] _segments;
        
        public Transform Player => player.Value;
        public float MovementSpeed => id.EnemyStats.movementSpeed;
        public float RotationSpeed => id.EnemyStats.rotationSpeed;

        private void OnEnable()
        {
            InitSegments();
        }

        private void InitSegments()
        {
            _segments = GetComponentsInChildren<SnakeSegment>(true);
            InitSegmentChain();
            SetSegmentsActive();
        }

        private void InitSegmentChain()
        {
            _segments[0].next = _segments[1];
            for (int i = 1; i < _segments.Length - 1; i++)
            {
                _segments[i].previous = _segments[i - 1];
                _segments[i].next = _segments[i + 1];
            }
            _segments[_segments.Length - 1].previous = _segments[_segments.Length - 2];
        }

        private void SetSegmentsActive()
        {
            for (int i = 0; i < _segments.Length; i++)
            {
                _segments[i].gameObject.SetActive(true);
            }
        }

        public Transform HeadSegment()
        {
            for (int i = 0; i < _segments.Length; i++)
            {
                if (_segments[i].IsHead) return _segments[i].transform;
            }
            return _segments[0].transform;
        }
    }
}