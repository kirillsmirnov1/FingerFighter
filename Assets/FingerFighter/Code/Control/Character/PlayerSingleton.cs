using UnityEngine;

namespace FingerFighter.Control.Character
{
    public class PlayerSingleton : MonoBehaviour // IMPR 
    {
        public static Transform Transform => _instance._transform;
        
        private static PlayerSingleton _instance;

        private Transform _transform;
        
        private void Awake()
        {
            _instance = this;
            _transform = transform;
        }
    }
}