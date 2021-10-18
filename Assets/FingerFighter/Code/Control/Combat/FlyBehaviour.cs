using FingerFighter.Control.Character;
using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Control.Combat
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class FlyBehaviour : MonoBehaviour // IMPR totally would need to refactor this 
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private FloatVariable movementSpeed;
        [SerializeField] private FloatVariable rotationSpeed;
        [SerializeField] private float angleOffset = -90;
        
        private Transform _player;
        private Transform _self;
        
        private Vector2 _directionToPlayer;
        private Vector2 _currentPos;

        private void OnValidate()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _self = transform;
            _player = PlayerSingleton.Transform;
        }

        private void FixedUpdate()
        {
            UpdateFields();
            Rotate();
            Move();
        }

        private void UpdateFields()
        {
            _currentPos = _self.position;
            Vector2 playerPos = _player.position; 
            _directionToPlayer = (playerPos - _currentPos).normalized;
        }

        private void Rotate()
        {
            var angle = Mathf.Atan2(_directionToPlayer.y, _directionToPlayer.x) * Mathf.Rad2Deg;
            var desiredRotation = Quaternion.Euler(0, 0, angle + angleOffset);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed);
        }

        private void Move()
        {
            var movement = (Vector2)_self.up * movementSpeed;
            rb.AddForce(movement, ForceMode2D.Force);
        }
    }
}