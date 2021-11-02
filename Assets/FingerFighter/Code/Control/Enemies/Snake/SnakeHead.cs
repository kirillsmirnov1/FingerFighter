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
            _segments = GetComponentsInChildren<SnakeSegment>();
        }

        public Transform HeadSegment()
        {
            for (int i = 0; i < _segments.Length; i++)
            {
                if (_segments[i].enabled) return _segments[i].transform;
            }
            return _segments[0].transform;
        }
    }
}