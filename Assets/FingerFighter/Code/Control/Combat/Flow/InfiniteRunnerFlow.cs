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
    // TODO extract abstract Flow
    // TODO refactor into SO 
    public class InfiniteRunnerFlow : MonoBehaviour  
    {
        [Header("Params")]
        [SerializeField] public float roomDuration = 25f;
        [SerializeField] public float restDuration = 25f;
        
        [Header("Game Objects")]
        [SerializeField] public EnemySpawnFormation spawn;
        [SerializeField] private EnemyFormationPackProvider enemyProvider;

        [Header("Data")]
        [SerializeField] public FloatVariable combatTimeScale;
        
        private RunnerFlowState _state;
        
        private Queue<EnemyFormation> _formations;
        [HideInInspector] public string currentPack;
        private Action _onUpdate;

        public EnemyFormation NextFormation 
            => _formations.Dequeue();

        private void Awake()
        {
            AliveEnemiesCounter.OnNoEnemiesLeftAlive += NoEnemiesLeft;
            PlayerStatus.OnDeath += PauseFlow;
            PlayerStatus.OnAlive += ResumeFlow;
        }

        private void OnDestroy()
        {
            AliveEnemiesCounter.OnNoEnemiesLeftAlive -= NoEnemiesLeft;
            PlayerStatus.OnDeath -= PauseFlow;
            PlayerStatus.OnAlive -= ResumeFlow;
        }

        private void OnEnable()
        {
            ResumeFlow();
            UpdateFormationsQueue();
            NextWave();
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
            currentPack = pack.ID;
            _formations = new Queue<EnemyFormation>(pack.Formations);
        }

        private void NoEnemiesLeft() 
            => _state?.NoEnemiesLeft();

        public void NextWave()
        {
            _state = null;
            if (_formations.Count > 1)
            {
                _state = new EnemyWaveLimitedTime(this);
            }
            else if(_formations.Count == 1)
            {
                _state = new BossWave(this);
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