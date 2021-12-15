using System;
using System.Collections.Generic;
using FingerFighter.Control.Common.Combat.Status;
using FingerFighter.Control.Common.Factories.EnemySpawn;
using FingerFighter.Model.EnemyFormations;
using UnityEngine;
using UnityUtils.Extensions;
using UnityUtils.Variables;

namespace FingerFighter.Control.Runner.Flow
{
    public abstract class ARunnerFlow : ScriptableObject 
    {
        public static event Action OnPlayerWon;
        
        [Header("Params")]
        [SerializeField] public float roomDuration = 25f;
                
        [Header("Game Objects")]
        [SerializeField] private GameObjectVariable spawnVariable;
        [SerializeField] protected EnemyFormationPackProvider enemyProvider;
        [SerializeField] private TransformVariable player;
        
        [Header("Data")]
        [SerializeField] public FloatVariable combatTimeScale;
        
        public EnemySpawnFormation Spawn { get; private set; }
        
        protected RunnerFlowState state;

        protected Queue<EnemyFormation> formations;
        [HideInInspector] public string currentPack;
        private Action _onUpdate;
        private MonoBehaviour _proxy;

        public EnemyFormation NextFormation 
            => formations.Dequeue();

        public void Init(MonoBehaviour proxy) 
            => _proxy = proxy;

        public void OnAwake()
        {
            Spawn = spawnVariable.Value.GetComponent<EnemySpawnFormation>();
            AliveEnemiesCounter.OnNoEnemiesLeftAlive += NoEnemiesLeft;
            PlayerStatus.OnDeath += PauseFlow;
            PlayerStatus.OnAlive += ResumeFlow;
        }

        public void OnDestroyed()
        {
            AliveEnemiesCounter.OnNoEnemiesLeftAlive -= NoEnemiesLeft;
            PlayerStatus.OnDeath -= PauseFlow;
            PlayerStatus.OnAlive -= ResumeFlow;
        }

        public void OnEnabled()
        {
            ResumeFlow();
            UpdateFormationsQueue();
            GoToNextWave();
        }

        public void OnUpdate() 
            => _onUpdate?.Invoke();

        public void GoToNextWave()
        {
            state = null;
            if (formations.Count > 1)
            {
                state = new EnemyWaveLimitedTime(this);
            }
            else if(formations.Count == 1)
            {
                state = new EnemyWave(this);
            }
            else
            {
                OnNoFormationsLeft();
            }
            _proxy.DelayAction(0f, () => state?.Enter());
        }

        protected abstract void UpdateFormationsQueue();

        protected abstract void OnNoFormationsLeft();

        private void ResumeFlow() 
            => _onUpdate = () => state?.Update();

        private void PauseFlow() 
            => _onUpdate = null;

        private void NoEnemiesLeft() 
            => state?.NoEnemiesLeft();

        protected void PlayerWon()
        {
            player.Value.gameObject.SetActive(false);
            OnPlayerWon?.Invoke();
        }
    }
}