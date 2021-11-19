using UnityEngine;

namespace FingerFighter.Control.Enemies.Snake
{
    public class SnakeSegmentDetachesWhenHead : SnakeSegment
    {
        [SerializeField] private MonoBehaviour[] reserveBehaviours;

        protected override void OnEnable()
        {
            SetReserveBehavioursEnabled(false);
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
                SetReserveBehavioursEnabled(true);
            }
        }

        private void SetReserveBehavioursEnabled(bool isEnabled)
        {
            for (int i = 0; i < reserveBehaviours.Length; i++)
            {
                reserveBehaviours[i].enabled = isEnabled;
            }
        }
    }
}