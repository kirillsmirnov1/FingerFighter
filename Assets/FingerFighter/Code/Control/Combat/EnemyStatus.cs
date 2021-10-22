using FingerFighter.Control.Factories.EnemySpawn;

namespace FingerFighter.Control.Combat
{
    public class EnemyStatus : CombatEntityStatus
    {
        protected override void OnDisable()
        {
            base.OnDisable();
            AEnemySpawn.ReturnToPool(gameObject, id.EnemyType);
        }
    }
}