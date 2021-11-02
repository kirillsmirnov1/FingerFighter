using System;
using UnityEngine;

namespace FingerFighter.Control.Combat.Health
{
    public class AHealth : MonoBehaviour
    {
        [SerializeField] private float baseHealth;
        public virtual float BaseHealth => baseHealth;

        public float CurrentHealth { get; protected set; }
        protected readonly object Lock = new object();

        public event Action<float> onHealthChange;
        public event Action onNoHealth;
        
        protected virtual void OnEnable()
        {
            CurrentHealth = BaseHealth;
        }

        public void Change(float healthChange)
        {
            lock (Lock)
            {
                CurrentHealth = Mathf.Max(0, CurrentHealth + healthChange);
                NotifyOnHealthChange();
            }
        }

        protected void NotifyOnHealthChange()
        {
            onHealthChange?.Invoke(CurrentHealth);
            if (CurrentHealth < 0.0001f) onNoHealth?.Invoke();
        }
    }
}