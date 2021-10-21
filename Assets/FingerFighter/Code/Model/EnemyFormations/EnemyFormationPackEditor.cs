using System.Linq;
using UnityEngine;

namespace FingerFighter.Model.EnemyFormations
{
    public class EnemyFormationPackEditor : MonoBehaviour
    {
        [SerializeField] private EnemyFormationPack enemyFormationPack;
        [SerializeField] private EnemyFormationEditor[] editors;
        
        private void OnValidate()
        {
            UpdatePack();
        }

        public void UpdatePack()
        {
            if (UpdatePackEditorName())
            {
                UpdateEditors();
                OverwritePack();
            }
        }

        private bool UpdatePackEditorName()
        {
            if (enemyFormationPack == null)
            {
                Debug.LogError($"No pack on {gameObject.name}");
                return false;
            }

            name = enemyFormationPack.name;
            return true;
        }

        private void UpdateEditors()
        {
            editors = GetComponentsInChildren<EnemyFormationEditor>();
        }

        private void OverwritePack()
        {
            enemyFormationPack.formations = editors.Select(e => e.formation).ToArray();
        }
    }
}