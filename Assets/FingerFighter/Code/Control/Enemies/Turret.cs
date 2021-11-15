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
        
        private float _rotation;
        private float _timeForANextShot;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, SpawnPos);
        }

        private Vector3 SpawnPos => transform.position + (Vector3) (0.5f * Vector2Ext.DegreeToVector2(_rotation));

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
            _timeForANextShot = Time.time + shotDelay;
            _rotation = (_rotation + afterShotRotation) % 360f;
        }

        private void MakeAShot()
        {
            var newProjectile = EnemyPool.Get(projectileType.tag).GetComponent<Rigidbody2D>();
            
            newProjectile.position = SpawnPos;
            newProjectile.rotation = _rotation;
            newProjectile.AddForce(projectileImpulse * Vector2Ext.DegreeToVector2(_rotation), ForceMode2D.Impulse);
        }
    }
}