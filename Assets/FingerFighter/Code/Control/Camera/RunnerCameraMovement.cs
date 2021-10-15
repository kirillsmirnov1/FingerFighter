using UnityEngine;

namespace FingerFighter.View
{
    public class RunnerCameraMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;

        private Transform _transform;
        private Vector3 _up;
        
        private void Awake()
        {
            _transform = transform;
            _up = Vector3.up;
        }

        private void Update()
        {
            _transform.position += _up * (speed * Time.deltaTime);
        }
    }
}