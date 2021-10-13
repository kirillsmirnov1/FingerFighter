using System.Linq;
using TMPro;
using UnityEngine;

namespace FingerFighter.Sandbox
{
    public class HandleSpeed : MonoBehaviour
    {
        [SerializeField] private TextMeshPro txt;
        [SerializeField] private int cachedSpeedCount = 5;
        
        public float Speed { get; private set; }
        public Vector2 Direction { get; private set; }

        private Vector2 _prevPos;
        private float[] _cachedSpeed;
        private int _cachedSpeedIndex;

        private void Start()
        {
            _cachedSpeed = new float[cachedSpeedCount];
            _prevPos = transform.position;
        }

        private void Update()
        {
            UpdateDirection();
            UpdateSpeed();
            // DisplaySpeed();
        }

        private void UpdateDirection()
        {
            Vector2 curPos = transform.position;
            Direction = curPos - _prevPos;
            _prevPos = curPos;
        }

        private void UpdateSpeed()
        {
            var currentSpeed = Direction.magnitude / Time.deltaTime;
            
            _cachedSpeed[_cachedSpeedIndex] = currentSpeed;
            _cachedSpeedIndex = (_cachedSpeedIndex + 1) % cachedSpeedCount;
            
            Speed = _cachedSpeed.Sum() / cachedSpeedCount;
        }

        private void DisplaySpeed()
        {
            var speedStr = $"{(int)Speed}";
            txt.text = speedStr;
            txt.transform.rotation = Quaternion.identity;
        }
    }
}
