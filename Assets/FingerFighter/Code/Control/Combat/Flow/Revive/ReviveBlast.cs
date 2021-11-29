using FingerFighter.Model.Enemies;
using FingerFighter.Utils;
using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Control.Combat.Flow.Revive
{
    public class ReviveBlast : MonoBehaviour
    {
        [SerializeField] private float blastForce = 1;
        [SerializeField] private TransformVariable playerTransformVariable;

        private Transform _player;
        
        private void Awake()
        {
            _player = playerTransformVariable.Value;
        }

        public void Blast()
        {
            var enemies = FindObjectsOfType<EnemyComponents>(); // TODO get from cached objects 
            foreach (var enemy in enemies)
            {
                if(enemy.rb == null) continue;
                Vector2 playerToEnemy = enemy.transform.position - _player.transform.position;
                var magnitude = playerToEnemy.magnitude;
                var direction = playerToEnemy.normalized;
                enemy.rb.AddVelocityChange(direction * (blastForce / magnitude)); 
            }
        }
    }
}