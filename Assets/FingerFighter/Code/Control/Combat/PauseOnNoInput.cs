using System.Collections;
using FingerFighter.Control.Input.Handles;
using UnityEngine;
using UnityUtils;
using UnityUtils.Variables;

namespace FingerFighter.Control.Combat
{
    public class PauseOnNoInput : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] private float pauseTimeSpeed = 0.1f;
        [SerializeField] private FloatVariable combatTimeScale;
        
        // TODO fade 
        // TODO turrets
        // TODO camera 
        // TODO flow 
        // TODO go from current scale to desired (calculate how many steps you need 
        private void OnValidate() => this.CheckNullFields();

        private void Awake()
        {
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
            ChangeTimeFlow(pauseTimeSpeed, 1f);
        }

        private void Pause()
        {
            StopAllCoroutines();
            ChangeTimeFlow(1f, pauseTimeSpeed);
        }

        private void ChangeTimeFlow(float from, float to)
        {
            var changeDuration = 0.25f;
            var steps = 10;
            var wfs = new WaitForSeconds(changeDuration / steps);
            
            StartCoroutine(ChangeTimeCoroutine());

            IEnumerator ChangeTimeCoroutine()
            {
                for (float i = 0; i <= steps; i++)
                {
                    combatTimeScale.Value = Mathf.Lerp(from, to, i / steps);
                    yield return wfs;
                }
            }
        }
    }
}