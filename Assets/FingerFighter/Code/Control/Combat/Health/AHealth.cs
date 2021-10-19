using System;
using UnityEngine;

namespace FingerFighter.Control.Combat.Health
{
    public class AHealth : MonoBehaviour
    {
        [SerializeField] private float baseHealth;
        public virtual float BaseHealth => baseHealth;

        private float _currentHealth;
        private readonly object _lock = new object();

        public event Action<float> onHealthChange;
        public event Action onNoHealth;
        
        private void OnEnable()
        {
            _currentHealth = BaseHealth;
        }

        public void Change(float healthChange)
        {
            lock (_lock)
            {
                _currentHealth = Mathf.Max(0, _currentHealth + healthChange);
                onHealthChange?.Invoke(_currentHealth);
                if(_currentHealth < 0.0001f) onNoHealth?.Invoke();
            }
        }
    }
}