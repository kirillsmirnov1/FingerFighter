using FingerFighter.Model.EnemyFormations;
using UnityEngine;

namespace FingerFighter.Control.Factories.EnemySpawn
{
    public class EnemySpawnFormation : AEnemySpawn
    {
        private Camera _camera;
        private Vector2 _jumpToCameraGap;

        protected override void Awake()
        {
            base.Awake();
            InitValues();
        }

        private void InitValues()
        {
            _camera = Camera.main;
            _jumpToCameraGap = new Vector2(0, _camera.orthographicSize * 2f);
        }

        public void Spawn(EnemyFormation formation)
        {
            JumpToCamera(); 
            for (int i = 0; i < formation.entries.Length; i++)
            {
                SpawnEnemy(formation.entries[i].enemy.tag, formation.entries[i].pos);
            }
        }
        
        private void JumpToCamera()
        {
            var newPos = (Vector2) _camera.transform.position + _jumpToCameraGap;
            CurrentPos = transform.position = newPos;
        }
    }
}