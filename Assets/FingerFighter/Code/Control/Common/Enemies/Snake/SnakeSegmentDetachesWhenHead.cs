using UnityEngine;

namespace FingerFighter.Control.Common.Enemies.Snake
{
    public class SnakeSegmentDetachesWhenHead : SnakeSegment
    {
        [Header("Head detach")]
        [SerializeField] private MonoBehaviour chainedBehaviour;
        [SerializeField] private MonoBehaviour freeBehaviour;

        protected override void OnEnable()
        {
            SetChainedStatus(true);
            base.OnEnable();
        }

        protected override void SetTarget()
        {
            base.SetTarget();
            if (IsHead)
            {
                OnSegmentLoss();
                IsHead = false;
                enabled = false;
                SetChainedStatus(false);
            }
        }

        private void SetChainedStatus(bool isChained)
        {
            chainedBehaviour.enabled = isChained;
            freeBehaviour.enabled = !isChained;
        }
    }
}