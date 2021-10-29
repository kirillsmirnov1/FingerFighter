using FingerFighter.Control.Damage;
using FingerFighter.Model.Damage;
using UnityEngine;

namespace FingerFighter.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class GameSounds : MonoBehaviour
    {
        [SerializeField] private AudioClip[] enemyHitSounds;
        // TODO enemyDeathSounds
        // TODO playerDeath sound 
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();

            HitTaker.OnHitTaken += OnHitTaken;
        }

        private void OnDestroy()
        {
            HitTaker.OnHitTaken -= OnHitTaken;
        }

        private void OnHitTaken(HitData obj)
        {
            // TODO another sound for player hit 
            _audioSource.PlayOneShot(enemyHitSounds[Random.Range(0, enemyHitSounds.Length)]);
        }
    }
}