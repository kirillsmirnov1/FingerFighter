﻿using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Control.Common
{
    public class InfinityMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private float radius = 1f;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private FloatVariable combatTimeScale;
        
        private float _t;

        private void Update()
        {
            _t += Time.deltaTime * combatTimeScale;
            var t = speed * _t;
            rb.MovePosition(radius * new Vector2(Mathf.Sin(2 * t) / 2, Mathf.Cos(t)));
        }
    }
}