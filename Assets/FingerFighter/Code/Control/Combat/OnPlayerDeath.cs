using UnityEngine;
using UnityEngine.Events;

namespace FingerFighter.Control.Combat
{
    public class OnPlayerDeath : MonoBehaviour
    {
        [SerializeField] private UnityEvent actions;
        
        private void Awake() => PlayerStatus.OnDeath += OnDeath;
        private void OnDestroy() => PlayerStatus.OnDeath -= OnDeath;
        private void OnDeath() => actions.Invoke();
    }
}