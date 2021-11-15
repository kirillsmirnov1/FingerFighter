using FingerFighter.Control.Combat.Damage;
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
            _timeForANextShot = Time.time + shotDelay;
            _rotation = (_rotation + afterShotRotation) % 360f;
        }

        private void MakeAShot()
        {
            for (var i = 0; i < shotAngles.Length; i++)
            {
                var rotation = _rotation + shotAngles[i];
                
                var newProjectile = EnemyPool.Get(projectileType.tag, SpawnPos(rotation), rotation - 90)
                    .GetComponent<Rigidbody2D>();
                newProjectile.GetComponent<ProjectileHitProvider>().parentTurret = gameObject;
                newProjectile.gameObject.SetActive(true);
                newProjectile.AddForce(projectileImpulse * Vector2Ext.DegreeToVector2(rotation), ForceMode2D.Impulse);
                newProjectile.rotation = rotation - 90;
            }
        }
    }
}