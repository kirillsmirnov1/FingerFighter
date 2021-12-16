using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class FloatVarSetsPitch : MonoBehaviour
    {
        [SerializeField] private FloatVariable var;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AnimationCurve curve = AnimationCurve.Linear(0, 0.65f, 1, 1);

        private void OnValidate() 
            => audioSource ??= GetComponent<AudioSource>();

        private void OnEnable() 
            => SetPitch(var);

        private void Awake() 
            => var.OnChange += SetPitch;
    
        private void OnDestroy() 
            => var.OnChange -= SetPitch;

        private void SetPitch(float val) 
            => audioSource.pitch = curve.Evaluate(val);
    }
}
