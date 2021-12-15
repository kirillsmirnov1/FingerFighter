using System;
using FingerFighter.Control.Common.Combat.Status;
using FingerFighter.Model.Common.Enemies;
using UnityEngine;
using UnityUtils.Attributes;
using UnityUtils.Variables;

namespace FingerFighter.Control.Common.Enemies.Snake
{
    public class SnakeHead : MonoBehaviour
    {
        [SerializeField] private EnemyStatus status;
        [SerializeField] private EnemyComponents components;

        [SerializeField] private TargetTransformType type;
        [ConditionalField("type", compareValues: new object[]{TargetTransformType.Component})]
        [SerializeField] private Transform transformComponent;
        [ConditionalField("type", compareValues: new object[]{TargetTransformType.Variable})]
        [SerializeField] private TransformVariable transformVariable;

        private SnakeSegment[] _segments;
        public Transform Target { get; private set; }
        public Transform HeadSegment { get; private set; }

        public float MovementSpeed => components.stats.movementSpeed;

        private void OnValidate()
        {
            components ??= GetComponent<EnemyComponents>();
        }

        private void Awake()
        {
            InitTarget();
        }

        public void InitTarget()
        {
            Target = type switch
            {
                TargetTransformType.Variable => transformVariable.Value,
                TargetTransformType.Component => transformComponent,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void MoveToTargetPos()
        {
            transform.position = Target.position;
        }

        private void OnEnable()
        {
            InitSegments();
        }

        private void InitSegments()
        {
            _segments = GetComponentsInChildren<SnakeSegment>(true);
            InitSegmentChain();
            InitSegmentPositions();
            SetSegmentsActive();
            UpdateHeadSegment();
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

        private void InitSegmentPositions()
        {
            var gap = -_segments[0].transform.up * _segments[0].GetComponent<CircleCollider2D>().radius * 2;
            for (int i = 0; i < _segments.Length; i++)
            {
                _segments[i].transform.localPosition = gap * i;
            }
        }

        private void SetSegmentsActive()
        {
            for (int i = 0; i < _segments.Length; i++)
            {
                _segments[i].gameObject.SetActive(true);
                _segments[i].enabled = true;
            }
        }

        public void UpdateHeadSegment()
        {
            HeadSegment = NewHeadSegment();
            if (status != null) status.deathPosTransform = HeadSegment;
        }

        private Transform NewHeadSegment()
        {
            for (var i = 0; i < _segments.Length; i++)
            {
                if (_segments[i].IsHead)
                {
                    return _segments[i].transform;
                }
            }
            for (var i = 0; i < _segments.Length; i++)
            {
                if (_segments[i].gameObject.activeSelf)
                {
                    return _segments[i].transform;
                }
            }
            return _segments[0].transform;
        }

        public enum TargetTransformType
        {
            Variable,
            Component,
        }
    }
}