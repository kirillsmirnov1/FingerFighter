using UnityEngine;

namespace FingerFighter.Control.Enemies.Behaviour
{
    public abstract class AEnemyBehaviour : ScriptableObject
    {
        public abstract void Apply(EnemyBehaviourMachine enemy);
    }
}