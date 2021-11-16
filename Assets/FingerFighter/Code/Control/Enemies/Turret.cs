﻿using System;
using FingerFighter.Control.Factories;
using FingerFighter.Model.Combat;
using FingerFighter.Model.Util;
using UnityEngine;

namespace FingerFighter.Control.Enemies
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private float shotDelay = .3f;
        [SerializeField] private float afterShotRotation = 10;
        [SerializeField] private EnemyStats projectileType;
        [SerializeField] private float projectileImpulse = 10;
        [SerializeField] private float[] shotAngles = {0f, 120, 240};
        
        private float _rotation;
        private float _timeForANextShot;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            for (int i = 0; i < shotAngles.Length; i++)
            {
                Gizmos.DrawLine(transform.position, SpawnPos(_rotation + shotAngles[i]));
            }
        }

        private Action _incrementRotation;
        
        private void Awake()
        {
            _incrementRotation = CircularRotation;
        }
        
        private Vector3 SpawnPos(float rotation) 
            => transform.position + (Vector3) (0.5f * Vector2Ext.DegreeToVector2(rotation));

        private void Update()
        {
            if (Time.time > _timeForANextShot)
            {
                MakeAShot();
                IncrementParams();
            }
        }

        private void IncrementParams()
        {
            IncrementTime();
            _incrementRotation?.Invoke();
        }

        private void IncrementTime()
        {
            _timeForANextShot = Time.time + shotDelay;
        }

        private void CircularRotation()
        {
            _rotation = (_rotation + afterShotRotation) % 360f;
        }

        private void MakeAShot()
        {
            for (var i = 0; i < shotAngles.Length; i++)
            {
                var rotation = _rotation + shotAngles[i];
                var impulse = projectileImpulse * Vector2Ext.DegreeToVector2(rotation);
                
                var newShot = EnemyPool.Get(projectileType.tag, SpawnPos(rotation));
                var newProjectile = newShot.GetComponent<Projectile>(); // FIXME find a way of doing it one way without any npe 
                if(newProjectile != null)
                {
                    newProjectile.Init(
                        gameObject, 
                        impulse,
                        rotation - 90
                        );
                }
                else
                {
                    newShot.SetActive(true); 
                    newShot.GetComponent<Rigidbody2D>().AddForce(impulse, ForceMode2D.Impulse);
                }
            }
        }
    }
}