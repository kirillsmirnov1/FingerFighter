﻿using FingerFighter.Control.Factories;

namespace FingerFighter.Control.Combat
{
    public class EnemyStatus : CombatEntityStatus
    {
        protected override void OnDisable()
        {
            base.OnDisable();
            RunnerEnemySpawn.ReturnToPool(gameObject, id.EnemyType);
        }
    }
}