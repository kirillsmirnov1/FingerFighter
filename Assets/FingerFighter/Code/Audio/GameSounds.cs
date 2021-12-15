using FingerFighter.Control.Common.Combat.Damage;
using FingerFighter.Control.Common.Combat.Status;
using FingerFighter.Model.Common;
using FingerFighter.Model.Common.Combat.Damage;
using UnityEngine;

namespace FingerFighter.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class GameSounds : MonoBehaviour
    {
        [SerializeField] private AudioClip playerDeath;
        [SerializeField] private AudioClip[] enemyHitSounds;
        [SerializeField] private AudioClip[] playerHitSounds;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();

            HitTaker.OnHitTaken += OnHitTaken;
            PlayerStatus.OnDeath += OnPlayerDeath;
        }

        private void OnDestroy()
        {
            HitTaker.OnHitTaken -= OnHitTaken;
            PlayerStatus.OnDeath -= OnPlayerDeath;
        }

        private void OnPlayerDeath()
        {
            _audioSource.PlayOneShot(playerDeath);
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