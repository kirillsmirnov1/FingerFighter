using FingerFighter.Control.Damage;
using FingerFighter.Model.Damage;
using UnityEngine;

namespace FingerFighter.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class GameSounds : MonoBehaviour
    {
        [SerializeField] private AudioClip[] enemyHitSounds;
        [SerializeField] private AudioClip[] playerHitSounds;
        
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

        private void OnHitTaken(HitData hitData)
        {
            var clip = hitData.Affected == Affiliation.Player
                ? playerHitSounds[Random.Range(0, playerHitSounds.Length)]
                : enemyHitSounds[Random.Range(0, enemyHitSounds.Length)];
            _audioSource.PlayOneShot(clip);
        }
    }
}