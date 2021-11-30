using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FingerFighter.View.Util
{
    public class ReviveButtonScaler : MonoBehaviour
    {
        [SerializeField] private AnimationCurve curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        [SerializeField] private RectTransform transformToScale;
        [SerializeField] private RectTransform parentRect;
        [SerializeField] private float scaleDuration = 1f;
        [SerializeField] private int steps = 10;
        
        private bool _used;
        private WaitForSeconds _wfs;

        private void Awake()
        {
            _wfs = new WaitForSeconds(scaleDuration / steps);
        }

        private void OnEnable()
        {
            _used = false;
            transformToScale.localScale = Vector3.zero;
            RebuildLayout();
        }

        public void Scale()
        {
            if(_used) return;

            StartCoroutine(ScaleCoroutine());

            IEnumerator ScaleCoroutine()
            {
                for (float i = 0; i <= steps; i++)
                {
                    transformToScale.localScale = Vector3.one * curve.Evaluate(i/steps);
                    RebuildLayout();
                    yield return _wfs;
                }
            }
        }

        private void RebuildLayout()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(parentRect);
        }
    }
}