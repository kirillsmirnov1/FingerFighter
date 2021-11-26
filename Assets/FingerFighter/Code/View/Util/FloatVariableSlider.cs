using System;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils.Variables;

namespace FingerFighter.View.Util
{
    [RequireComponent(typeof(Slider))]
    public class FloatVariableSlider : MonoBehaviour // TODO move to UU
    {
        [SerializeField] private FloatVariable variable;
        [SerializeField] private Slider slider;

        private void OnValidate()
        {
            slider ??= GetComponent<Slider>();
        }

        private void Awake()
        {
            slider.value = variable.Value;
            slider.onValueChanged.AddListener(OnSliderValueChange);
        }

        private void OnDestroy()
        {
            slider.onValueChanged.RemoveListener(OnSliderValueChange);
        }

        private void OnSliderValueChange(float newValue)
        {
            variable.Value = newValue;
        }
    }
}
