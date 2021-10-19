using FingerFighter.Control.Enemies.Behaviour;
using FingerFighter.Model.Combat;
using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Control.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyBehaviourMachine : MonoBehaviour 
    {
        [SerializeField] public Rigidbody2D rb;
        [SerializeField] public float angleOffset = -90;
        [SerializeField] private TransformVariable playersTransform;
        [SerializeField] private CombatEntityId id;
        
        [Header("Behaviours")] 
        [SerializeField] private AEnemyBehaviour[] behaviours;

        public EnemyStats Stats => id.EnemyStats; 
        [HideInInspector] public Transform self;
        [HideInInspector] public Vector2 directionToPlayer;
        private Transform _player;
        private Vector2 _currentPos;

        private void OnValidate()
        {
            rb = GetComponent<Rigidbody2D>();
            id = GetComponent<CombatEntityId>();
        }

        private void OnEnable()
        {
            self = transform;
            _player = playersTransform.Value;
        }

        private void FixedUpdate()
        {
            UpdateFields();
            ApplyBehaviours();
        }

        private void UpdateFields()
        {
            _currentPos = self.position;
            Vector2 playerPos = _player.position; 
            directionToPlayer = (playerPos - _currentPos).normalized;
        }

        private void ApplyBehaviours()
        {
            for (var i = 0; i < behaviours.Length; i++)
            {
                behaviours[i].Apply(this);
            }
        }
    }
}