﻿using System.Collections.Generic;
using FingerFighter.Control.Combat.Status;
using FingerFighter.Model.Combat;
using FingerFighter.Model.EnemyFormations;
using UnityEngine;

namespace FingerFighter.Control.Factories.EnemySpawn
{
    public class EnemySpawnFormation : AEnemySpawn
    {
        [SerializeField] private EnemyFormationPackProvider enemyFormationPackProvider;

        private Queue<EnemyFormation> _formationQueue = new Queue<EnemyFormation>();

        private Camera _camera;
        private Vector2 _jumpToCameraGap;
        private int _aliveEnemies;

        protected override void Awake()
        {
            base.Awake();
            InitValues();
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void InitValues()
        {
            _camera = Camera.main;
            _jumpToCameraGap = new Vector2(0, _camera.orthographicSize) + jumpDirection / 2;
        }

        private void SubscribeToEvents()
        {
            EnemyStatus.OnSpawn += IncrementAliveEnemiesCounter;
            EnemyStatus.OnDeath += DecrementAliveEnemiesCounter;
        }

        private void UnsubscribeFromEvents()
        {
            EnemyStatus.OnSpawn -= IncrementAliveEnemiesCounter;
            EnemyStatus.OnDeath -= DecrementAliveEnemiesCounter;
        }

        private void IncrementAliveEnemiesCounter() 
            => _aliveEnemies++;

        private void DecrementAliveEnemiesCounter(EnemyDeathData obj)
        {
            _aliveEnemies--;
            if(_aliveEnemies <= 0) NoEnemiesLeftAlive();
        }

        protected override void Spawn() // TODO control spawn from Flow manager 
        {
            var formation = NextFormation().entries;
            for (int i = 0; i < formation.Length; i++)
            {
                SpawnEnemy(formation[i].enemy.tag, formation[i].pos);
            }
        }

        private EnemyFormation NextFormation()
        {
            if (_formationQueue.Count == 0)
            {
                _formationQueue = new Queue<EnemyFormation>(enemyFormationPackProvider.NextPack());
            }
            return _formationQueue.Dequeue();
        }

        private void NoEnemiesLeftAlive() 
            => JumpToCamera();

        private void JumpToCamera()
        {
            if(_camera == null) return;
            transform.position = (Vector2)_camera.transform.position + _jumpToCameraGap;
        }
    }
}