using TMPro;
using UnityEngine;

namespace FingerFighter.Sandbox
{
    public class HandleSpeed : MonoBehaviour
    {
        [SerializeField] private TextMeshPro txt;

        private Vector2 _prevPos;
        private float _velocity;

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
            _velocity = (curPos - _prevPos).magnitude / Time.deltaTime;
            _prevPos = curPos;
        }

        private void DisplayVelocity()
        {
            var velocityStr = $"{(int)_velocity}";
            txt.text = velocityStr;
            txt.transform.rotation = Quaternion.identity;
        }
    }
}
