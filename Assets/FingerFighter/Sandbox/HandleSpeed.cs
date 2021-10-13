using TMPro;
using UnityEngine;

namespace FingerFighter.Sandbox
{
    public class HandleSpeed : MonoBehaviour
    {
        [SerializeField] private TextMeshPro txt;

        private Vector2 _prevPos;
        public float Velocity { get; private set; }

        private void Start()
        {
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
            Velocity = (curPos - _prevPos).magnitude / Time.deltaTime;
            _prevPos = curPos;
        }

        private void DisplayVelocity()
        {
            var velocityStr = $"{(int)Velocity}";
            txt.text = velocityStr;
            txt.transform.rotation = Quaternion.identity;
        }
    }
}
