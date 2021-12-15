using FingerFighter.Control.Common.Conditions.Flags;
using UnityEngine;

namespace FingerFighter.Control.Common.Conditions.Triggers
{
    public abstract class ATrigger : MonoBehaviour
    {
        [SerializeField] private AFlag flag;

        private void OnEnable()
        {
            InvokeTrigger(flag.On);
            flag.OnChange += InvokeTrigger;
        }

        private void OnDisable()
        {
            flag.OnChange -= InvokeTrigger;
        }

        protected abstract void InvokeTrigger(bool flagIsOn);
    }
}