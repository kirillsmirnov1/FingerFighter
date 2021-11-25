using System.Collections;
using FingerFighter.Control.Input.Handles;
using UnityEngine;
using UnityUtils;
using UnityUtils.Variables;

namespace FingerFighter.Control.Combat
{
    public class PauseOnNoInput : MonoBehaviour
    {
        [Header("Settings")]
        [Range(0f, 0.999f)]
        [SerializeField] private float timeScaleAtPause = 0.1f;
        [Range(1, 100)]
        [SerializeField] private int maxStepsToPause = 10;
        [Range(0.001f, 0.1f)]
        [SerializeField] private float stepDuration = 0.025f;
        
        [Header("Variable")]
        [SerializeField] private FloatVariable combatTimeScale;
        private WaitForSeconds _wfs;

        [Header("Components")]
        [SerializeField] private SpriteRenderer fade;
        
        // TODO turrets
        // TODO camera 
        // TODO flow 
        private void OnValidate() => this.CheckNullFields();

        private void Awake()
        {
            _wfs = new WaitForSeconds(stepDuration);
            combatTimeScale.Value = 1f;
            HandlesInputManager.OnInputRegained += UnPause;
            HandlesInputManager.OnInputLost += Pause;
        }

        private void OnDestroy()
        {
            HandlesInputManager.OnInputRegained -= UnPause;
            HandlesInputManager.OnInputLost -= Pause;
            combatTimeScale.Value = 1f;
        }

        private void UnPause()
        {
            StopAllCoroutines();
            ChangeTimeFlow(combatTimeScale, 1f);
        }

        private void Pause()
        {
            StopAllCoroutines();
            ChangeTimeFlow(combatTimeScale, timeScaleAtPause);
        }

        private void ChangeTimeFlow(float from, float to)
        {
            var startAlpha = fade.color.a;
            var endAlpha = to >= from ? 0f : 1f;
            var fadeColor = Color.white;
            
            var totalScaleChange = 1f - timeScaleAtPause;
            var currentScaleChange = Mathf.Abs(to - from);
            var scaleChangeRatio = currentScaleChange / totalScaleChange;
            
            var steps = (int) (maxStepsToPause * scaleChangeRatio);

            StartCoroutine(ChangeTimeCoroutine());

            IEnumerator ChangeTimeCoroutine()
            {
                for (float i = 0; i < steps; i++)
                {
                    var t = i / steps;
                    combatTimeScale.Value = Mathf.Lerp(from, to, t);
                    fadeColor.a = Mathf.Lerp(startAlpha, endAlpha, t);
                    fade.color = fadeColor;
                    yield return _wfs;
                }

                fadeColor.a = endAlpha;
                fade.color = fadeColor;
                combatTimeScale.Value = to;
            }
        }
    }
}