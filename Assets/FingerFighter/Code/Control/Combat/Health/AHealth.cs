using System;
using UnityEngine;

namespace FingerFighter.Control.Combat.Health
{
    public abstract class AHealth : MonoBehaviour
    {
        public abstract float BaseHealth { get; }

        public bool NoHealth => CurrentHealth < 0.0001f;
        public virtual float CurrentHealth { get; protected set; }
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
            if (NoHealth) onNoHealth?.Invoke();
        }
    }
}