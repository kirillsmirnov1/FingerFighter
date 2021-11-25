using System;
using System.Collections.Generic;
using FingerFighter.Control.Combat.Status;
using FingerFighter.Control.Factories.EnemySpawn;
using FingerFighter.Model.EnemyFormations;
using UnityEngine;
using UnityUtils.Extensions;
using UnityUtils.Variables;

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

        [Header("Data")]
        [SerializeField] private FloatVariable combatTimeScale;
        
        private State _state;
        
        private Queue<EnemyFormation> _formations;
        private string _currentPack;
        private Action _onUpdate;

        private EnemyFormation NextFormation 
            => _formations.Dequeue();

        private void Awake()
        {
            AliveEnemiesCounter.OnNoEnemiesLeftAlive += NoEnemiesLeft;
            PlayerStatus.OnDeath += PauseFlow;
        }

        private void OnDestroy()
        {
            AliveEnemiesCounter.OnNoEnemiesLeftAlive -= NoEnemiesLeft;
            PlayerStatus.OnDeath -= PauseFlow;
        }

        private void OnEnable()
        {
            ResumeFlow();
            UpdateFormationsQueue();
            NextRoom();
        }

        private void ResumeFlow() 
            => _onUpdate = () => _state?.Update();

        private void PauseFlow() 
            => _onUpdate = null;

        private void Update() 
            => _onUpdate?.Invoke();

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