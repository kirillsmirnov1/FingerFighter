using UnityEngine;
using UnityEngine.UI;
using UnityUtils.Extensions;

namespace FingerFighter.View.Util
{
    public class ReviveButtonScaler : MonoBehaviour
    {
        [SerializeField] private AnimationCurve curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        [SerializeField] private RectTransform transformToScale;
        [SerializeField] private RectTransform parentRect;
        [SerializeField] private float scaleDuration = 1f;
        
        private bool _used;
        private float _scaleDurationLeft;
        
        private void OnEnable()
        {
            _used = false;
            transformToScale.localScale = Vector3.zero;
            this.DelayAction(0f, RebuildLayout);
        }

        public void Scale()
        {
            if(_used) return;
            _used = true;
            _scaleDurationLeft = scaleDuration;
        }

        private void Update()
        {
            if(_scaleDurationLeft <= 0) return;
            _scaleDurationLeft -= Time.deltaTime;
            
            transformToScale.localScale = Vector3.one * curve.Evaluate(1f - _scaleDurationLeft/scaleDuration);
            RebuildLayout();
        }

        private void RebuildLayout()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(parentRect);
        }
    }
}