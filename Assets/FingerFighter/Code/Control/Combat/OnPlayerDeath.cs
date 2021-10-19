using UnityEngine;
using UnityEngine.Events;

namespace FingerFighter.Control.Combat
{
    public class OnPlayerDeath : MonoBehaviour
    {
        [SerializeField] private UnityEvent actions;
        
        private void Awake() => PlayerStatus.OnDead += OnDeath;
        private void OnDestroy() => PlayerStatus.OnDead -= OnDeath;
        private void OnDeath() => actions.Invoke();
    }
}