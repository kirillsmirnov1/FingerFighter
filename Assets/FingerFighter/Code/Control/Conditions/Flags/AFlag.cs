using System;
using UnityEngine;

namespace FingerFighter.Control.Conditions.Flags
{
    public abstract class AFlag : MonoBehaviour
    {
        public event Action<bool> OnChange; 
        private bool _on;

        public bool On
        {
            get => _on;
            protected set
            {
                if(value == _on) return;
                _on = value;
                OnChange?.Invoke(_on);
            }
        }
    }
}