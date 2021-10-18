using FingerFighter.Control.Enemies.Behaviour;
using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Control.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyBehaviourMachine : MonoBehaviour // IMPR totally would need to refactor this 
    {
        [SerializeField] public Rigidbody2D rb;
        [SerializeField] public FloatVariable movementSpeed;
        [SerializeField] public FloatVariable rotationSpeed;
        [SerializeField] public float angleOffset = -90;
        [SerializeField] private TransformVariable playersTransform;
        
        [Header("Behaviours")] 
        [SerializeField] private AEnemyBehaviour[] behaviours;
        
        [HideInInspector] public Transform self;
        [HideInInspector] public Vector2 directionToPlayer;
        private Transform _player;
        private Vector2 _currentPos;

        private void OnValidate()
        {
            rb = GetComponent<Rigidbody2D>();
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