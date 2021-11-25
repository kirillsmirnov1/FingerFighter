using FingerFighter.Control.Enemies.Behaviour.Projectiles;
using FingerFighter.Model.Combat;
using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Model.Enemies
{
    public class EnemyComponents : MonoBehaviour
    {
        [SerializeField] public Rigidbody2D rb;
        [SerializeField] public Projectile projectile;
        
        [Header("Data")]
        [SerializeField] protected TransformVariable player;
        [SerializeField] public FloatVariable combatTimeScale;
        [SerializeField] public EnemyStats stats;
        
        public Transform Target { get; private set; }
        public string EnemyType => stats.tag;

        private void OnValidate()
        {
            rb ??= GetComponent<Rigidbody2D>();
            projectile ??= GetComponent<Projectile>();

            if (stats == null && gameObject.name != "_EnemyCombatEntity")
            {
                Debug.LogWarning($"No stats on {gameObject.name}");
            }
        }

        private void Awake()
        {
            Target = player.Value;
        }

        public void OverrideTarget(Transform target)
            => Target = target;
    }
}