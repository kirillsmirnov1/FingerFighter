using System;
using TMPro;
using UnityEngine;

namespace FingerFighter.Sandbox
{
    public class HandleSpeed : MonoBehaviour
    {
        [SerializeField] private TextMeshPro txt;
        [SerializeField] private Rigidbody2D rb;
        
        private void OnValidate()
        {
            rb = GetComponent<Rigidbody2D>();
            txt = GetComponentInChildren<TextMeshPro>();
        }

        private void FixedUpdate()
        {
            txt.text = $"{rb.velocity.magnitude:0.00}";
        }
    }
}
