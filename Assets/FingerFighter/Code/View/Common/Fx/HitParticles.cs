using FingerFighter.Control.Common.Combat.Damage;
using FingerFighter.Model.Common;
using FingerFighter.Model.Common.Combat.Damage;
using UnityEngine;
using UnityUtils.Extensions;

namespace FingerFighter.View.Common.Fx
{
    [RequireComponent(typeof(ParticleSystem))]
    public class HitParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem ps;

        private void OnValidate()
        {
            ps = GetComponent<ParticleSystem>();
        }

        private void Awake()
        {
            HitTaker.OnHitTaken += ShootParticles;
        }

        private void OnDestroy()
        {
            HitTaker.OnHitTaken -= ShootParticles;
        }

        private void ShootParticles(HitData hitData)
        {
            if(hitData.Affected == Affiliation.Player) return;
            transform.rotation = Quaternion.Euler(QuaternionExt.LookRotation2DAngle(hitData.Direction, 180), -90, 90 );
            transform.position = hitData.Position;
            ps.Play();
        }
    }
}