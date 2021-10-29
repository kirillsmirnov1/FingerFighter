using System.Linq;
using TMPro;
using UnityEngine;

namespace FingerFighter.Control.Character.Handles
{
    public class HandleSpeed : MonoBehaviour
    {
        [SerializeField] private TextMeshPro txt;
        [SerializeField] private int cacheSize = 5;
        
        public float Speed { get; private set; }
        public Vector2 Direction { get; private set; }

        private Vector2 _prevPos;
        private Vector2 _currentDirection;
        private float[] _cachedSpeed;
        private Vector2[] _cachedDirection;
        private int _cacheIndex;

        private void Start()
        {
            _cachedSpeed = new float[cacheSize];
            _cachedDirection = new Vector2[cacheSize];
            _prevPos = transform.position;
        }

        private void Update()
        {
            UpdateDirection();
            UpdateSpeed();
            _cacheIndex = (_cacheIndex + 1) % cacheSize;
        }

        private void UpdateDirection()
        {
            Vector2 curPos = transform.position;
            _currentDirection = curPos - _prevPos;
            _prevPos = curPos;

            _cachedDirection[_cacheIndex] = _currentDirection;
            
            var sumDir = Vector2.zero;
            for (var i = 0; i < _cachedDirection.Length; i++)
            {
                var direction = _cachedDirection[i];
                sumDir += direction;
            }
            sumDir /= cacheSize;
            Direction = sumDir;
        }

        private void UpdateSpeed()
        {
            var currentSpeed = _currentDirection.magnitude / Time.deltaTime;
            
            _cachedSpeed[_cacheIndex] = currentSpeed;

            Speed = _cachedSpeed.Sum() / cacheSize;
        }

        private void DisplaySpeed()
        {
            var speedStr = $"{(int)Speed}";
            txt.text = speedStr;
            txt.transform.rotation = Quaternion.identity;
        }
    }
}
