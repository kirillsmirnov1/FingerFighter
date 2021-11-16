using System.Linq;
using FingerFighter.Model.Combat;
using UnityEngine;

namespace FingerFighter.Model.EnemyFormations
{
    public class EnemyFormationPackEditor : MonoBehaviour
    {
        [SerializeField] private EnemyFormationPack enemyFormationPack;
        [SerializeField] private string id;
        [SerializeField] private EnemyStats boss;
        [SerializeField] private EnemyFormationEditor[] editors;
#if UNITY_EDITOR
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
            foreach (var editor in editors)
            {
                editor.UpdateName();
            }
        }

        private void OverwritePack() 
            => enemyFormationPack.Overwrite(id, Formations, boss);

        private EnemyFormation[] Formations 
            => editors.Select(e => e.formation).ToArray();
#endif
    }
}