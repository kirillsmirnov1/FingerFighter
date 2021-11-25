using System;
using FingerFighter.Control.Combat.Status;
using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Control.Cam
{
    public class RunnerCameraMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private FloatVariable combatTimeScale;

        private Transform _transform;
        private Vector3 _up;
        private Action _onUpdate;

        private void Awake()
        {
            _transform = transform;
            _up = Vector3.up;
            
            OnPlayerAlive();
            
            PlayerStatus.OnAlive += OnPlayerAlive;
            PlayerStatus.OnDeath += OnPlayerDead;
        }

        private void OnDestroy()
        {
            PlayerStatus.OnAlive -= OnPlayerAlive;
            PlayerStatus.OnDeath -= OnPlayerDead;
        }

        private void Update() => _onUpdate?.Invoke();
        
        private void OnPlayerAlive() => _onUpdate = Move;
        private void OnPlayerDead() => _onUpdate = null;
        
        private void Move() => _transform.position += _up * (speed * Time.deltaTime * combatTimeScale);
    }
}