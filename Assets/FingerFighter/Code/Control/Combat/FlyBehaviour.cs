using System;
using FingerFighter.Control.Character;
using UnityEngine;

namespace FingerFighter.Control.Combat
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class FlyBehaviour : MonoBehaviour // IMPR totally would need to refactor this 
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed = 0.01f;

        private Transform _player;
        private Transform _self;

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
            Move();
        }

        private void Move()
        {
            Vector2 currentPos = _self.position;
            Vector2 playerPos = _player.position;
            var toPlayer = (playerPos - currentPos).normalized;
            var movement = toPlayer * speed;
            rb.MovePosition(currentPos + movement);
        }
    }
}