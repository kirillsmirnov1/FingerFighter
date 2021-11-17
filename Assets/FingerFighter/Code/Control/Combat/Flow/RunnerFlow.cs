using System.Collections.Generic;
using FingerFighter.Control.Factories.EnemySpawn;
using FingerFighter.Model.EnemyFormations;
using UnityEngine;
using UnityUtils.Extensions;

namespace FingerFighter.Control.Combat.Flow
{
    public partial class RunnerFlow : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private float roomDuration = 25f;
        [SerializeField] private float restDuration = 25f;
        
        [Header("Game Objects")]
        [SerializeField] private EnemySpawnFormation spawn;
        [SerializeField] private EnemyFormationPackProvider enemyProvider;

        private State _state;
        
        private Queue<EnemyFormation> _formations;
        private string _currentPack;
        
        private EnemyFormation NextFormation 
            => _formations.Dequeue();

        private void Awake() 
            => AliveEnemiesCounter.OnNoEnemiesLeftAlive += NoEnemiesLeft;

        private void OnDestroy() 
            => AliveEnemiesCounter.OnNoEnemiesLeftAlive -= NoEnemiesLeft;

        private void OnEnable()
        {
            UpdateFormationsQueue();
            NextRoom();
        }

        private void Update() 
            => _state?.Update();

        private void UpdateFormationsQueue()
        {
            var pack = enemyProvider.NextPack();
            _currentPack = pack.ID;
            _formations = new Queue<EnemyFormation>(pack.Formations);
        }

        private void NoEnemiesLeft() 
            => _state?.NoEnemiesLeft();

        private void NextRoom()
        {
            _state = null;
            if (_formations.Count > 1)
            {
                _state = new Room(this);
            }
            else if(_formations.Count == 1)
            {
                _state = new Boss(this);
            }
            else
            {
                _state = new Rest(this);
                UpdateFormationsQueue();
            }
            this.DelayAction(0f, () => _state.Enter());
        }
    }
}