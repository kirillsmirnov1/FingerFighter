﻿using System;
using FingerFighter.Control.Factories;
using FingerFighter.Model.Combat;
using UnityEngine;
using UnityUtils.Attributes;
using UnityUtils.Extensions;

namespace FingerFighter.Control.Enemies
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private float startDelay;
        [SerializeField] private float shotDelay = .3f;
        [SerializeField] private float afterShotRotation = 10;
        [SerializeField] private EnemyStats projectileType;
        [SerializeField] private float projectileImpulse = 10;
        [SerializeField] private Color projectileColor;
        [SerializeField] private RotationMode rotationMode;
        [ConditionalField("rotationMode", compareValues: new object[] {RotationMode.Arc})]
        [SerializeField] private Vector2 arcFromTo = new Vector2(225, 315);
        [SerializeField] private float[] shotAngles = {0f, 120, 240};
        
        private float _rotation;
        private float _timeForANextShot;
        private Action _incrementRotation;

        private void OnDrawGizmos()
        {
            var localRotation = transform.rotation.eulerAngles.z + _rotation;
            switch (rotationMode)
            {
                case RotationMode.Circular:
                    Gizmos.color = Color.yellow;
                    for (int i = 0; i < shotAngles.Length; i++)
                    {
                        Gizmos.DrawLine(transform.position, SpawnPos(localRotation + shotAngles[i]));
                    }
                    break;
                case RotationMode.Arc:
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(transform.position, SpawnPos(arcFromTo.x, 1f));
                    Gizmos.DrawLine(transform.position, SpawnPos(arcFromTo.y, 1f));
                    Gizmos.color = Color.yellow;
                    for (int i = 0; i < shotAngles.Length; i++)
                    {
                        if(Application.isPlaying)
                            Gizmos.DrawLine(transform.position, SpawnPos(localRotation + shotAngles[i]));
                        else
                            Gizmos.DrawLine(transform.position, SpawnPos(localRotation + shotAngles[i] + arcFromTo.x));
                    }
                    break;
            }
        }

        private void OnValidate()
        {
            if(projectileColor.a == 0)
                projectileColor = GetComponent<SpriteRenderer>().color;
        }

        private void OnEnable()
        {
            _timeForANextShot = Time.time + startDelay;
        }

        private void Awake()
        {
            _incrementRotation = rotationMode switch
            {
                RotationMode.Circular => CircularRotation,
                RotationMode.Arc => ArcRotation,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        private Vector3 SpawnPos(float rotation, float magnitude = 0.5f) 
            => transform.position + (Vector3) (magnitude * Vector2Ext.DegreeToVector2(rotation));

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

        private void ArcRotation()
        {
            _rotation = Mathf.Clamp(_rotation + afterShotRotation, arcFromTo.x, arcFromTo.y);
            if (_rotation == arcFromTo.x || _rotation == arcFromTo.y)
            {
                afterShotRotation = -afterShotRotation;
            }
        }

        private void MakeAShot()
        {
            var turretRotation = transform.rotation.eulerAngles.z;
            for (var i = 0; i < shotAngles.Length; i++)
            {
                var rotation = _rotation + turretRotation + shotAngles[i];
                var impulse = projectileImpulse * Vector2Ext.DegreeToVector2(rotation);
                
                var newShot = EnemyPool.Get(projectileType.tag, SpawnPos(rotation));
                var newProjectile = newShot.projectile;
                if(newProjectile != null)
                {
                    newProjectile.Init(
                        gameObject, 
                        projectileColor,
                        impulse,
                        rotation - 90
                        );
                }
                else
                {
                    newShot.gameObject.SetActive(true); 
                    newShot.rb.AddForce(impulse, ForceMode2D.Impulse);
                }
            }
        }
        
        public enum RotationMode
        {
            Circular,
            Arc,
        }
    }
}