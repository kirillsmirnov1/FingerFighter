using System;
using UnityEngine;

namespace FingerFighter.Audio
{
    public class AudioCrossFade : MonoBehaviour // IMPR that shouldn't be monobehaviour 
    {
        private float _duration;
        private float _timeLeft;
        
        private AudioSource _from;
        private AudioSource _to;
        private Action _onUpdate;

        public void BeginCrossFade(AudioSource from, AudioSource to, float crossFadeDuration = 0.5f)
        {
            _duration = _timeLeft = crossFadeDuration;
            _from = from;
            _to = to;
            _to.volume = 0f;
            _to.Play();
            _onUpdate = CrossFade;
        }

        private void CrossFade()
        {
            _timeLeft -= Time.deltaTime;
            _from.volume = Mathf.InverseLerp(0f, _duration, _timeLeft);
            _to.volume = Mathf.InverseLerp(_duration, 0f, _timeLeft);

            if (_timeLeft <= 0f)
            {
                EndCrossFade();
            }
        }

        private void EndCrossFade()
        {
            _onUpdate = null;
            _from.Pause();
        }

        private void Update()
        {
            _onUpdate?.Invoke();
        }
        
        
    }
}