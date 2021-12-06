using System;
using System.Collections.Generic;
using FingerFighter.Control.Combat.Status;
using FingerFighter.Control.Factories.EnemySpawn;
using FingerFighter.Model.EnemyFormations;
using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Control.Combat.Flow
{
    public abstract class ARunnerFlow : MonoBehaviour // TODO refactor into SO 
    {
        [Header("Params")]
        [SerializeField] public float roomDuration = 25f;
                
        [Header("Game Objects")]
        [SerializeField] public EnemySpawnFormation spawn;
        [SerializeField] protected EnemyFormationPackProvider enemyProvider;
        
        [Header("Data")]
        [SerializeField] public FloatVariable combatTimeScale;
        
        protected RunnerFlowState state;

        protected Queue<EnemyFormation> formations;
        [HideInInspector] public string currentPack;
        private Action _onUpdate;

        public EnemyFormation NextFormation 
            => formations.Dequeue();
        
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

        protected void OnEnable()
        {
            ResumeFlow();
            UpdateFormationsQueue();
            GoToNextWave();
        }
        
        public abstract void GoToNextWave();

        protected abstract void UpdateFormationsQueue();

        private void ResumeFlow() 
            => _onUpdate = () => state?.Update();

        private void PauseFlow() 
            => _onUpdate = null;

        private void Update() 
            => _onUpdate?.Invoke();

        private void NoEnemiesLeft() 
            => state?.NoEnemiesLeft();
    }
}