using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace FingerFighter.Sandbox
{
    public class HandleSpeed : MonoBehaviour
    {
        [SerializeField] private TextMeshPro txt;
        [SerializeField] private int numberOfCachedVelocities = 5;
        
        public float Velocity { get; private set; }
        
        private Vector2 _prevPos;
        private float[] _cachedVelocities;
        private int _cvIndex;

        private void Start()
        {
            _cachedVelocities = new float[numberOfCachedVelocities];
            _prevPos = transform.position;
        }

        private void Update()
        {
            UpdateVelocity();
            DisplayVelocity();
        }

        private void UpdateVelocity()
        {
            Vector2 curPos = transform.position;
            var curVel = (curPos - _prevPos).magnitude / Time.deltaTime;
            _prevPos = curPos;
            
            _cachedVelocities[_cvIndex] = curVel;
            _cvIndex = (_cvIndex + 1) % numberOfCachedVelocities;
            
            Velocity = _cachedVelocities.Sum() / numberOfCachedVelocities;
        }

        private void DisplayVelocity()
        {
            var velocityStr = $"{(int)Velocity}";
            txt.text = velocityStr;
            txt.transform.rotation = Quaternion.identity;
        }
    }
}
