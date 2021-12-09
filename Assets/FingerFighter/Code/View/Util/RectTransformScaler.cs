using System;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils.Extensions;

namespace FingerFighter.View.Util
{
    public class RectTransformScaler : MonoBehaviour
    {
        [SerializeField] private AnimationCurve curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        [SerializeField] private RectTransform transformToScale;
        [SerializeField] private RectTransform parentRect;
        [SerializeField] private float scaleDuration = 1f;
        [Tooltip("Should be either -1 or 1")]
        [Range(-1f, 1f)]
        [SerializeField] private float scaleDirection = 1f;
        [SerializeField] private bool minimizeOnEnable = true;
        
        private bool _used;
        private float _t;

        private Func<bool> _shouldStop;

        private void OnValidate()
        {
            scaleDirection = Mathf.Sign(scaleDirection);
        }

        private void OnEnable()
        {
            _used = false;
            Scale();
            if(minimizeOnEnable) transformToScale.localScale = Vector3.zero;
            this.DelayAction(0f, RebuildLayout);
        }

        private void InitStopper()
        {
            _shouldStop = scaleDirection > 0 
                ? (Func<bool>)(() => _t >= scaleDuration)
                : (Func<bool>)(() => _t <= 0f);
        }

        public void ScaleOnce()
        {
            if(_used) return;
            _used = true;
            Scale();
        }

        public void Scale()
        {
            scaleDirection = -scaleDirection;
            InitStopper();
        }

        private void Update()
        {
            if(_shouldStop()) return;
            _t += scaleDirection * Time.deltaTime;
            
            transformToScale.localScale = Vector3.one * curve.Evaluate(_t/scaleDuration);
            RebuildLayout();
        }

        private void RebuildLayout()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(parentRect);
        }
    }
}