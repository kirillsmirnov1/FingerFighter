using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FingerFighter.Model.EnemyFormations
{
    public class EnemyFormationEditor : MonoBehaviour
    {
        [SerializeField] private EnemyFormationPackEditor packEditor;
        [SerializeField] public EnemyFormation formation;
#if UNITY_EDITOR
        
        private void OnValidate()
        {
            packEditor = GetComponentInParent<EnemyFormationPackEditor>();
            UpdatePack();
        }

        public void UpdateName()
        {
            var index = transform.GetSiblingIndex() + 1;
            gameObject.name = $"#{index:000} : P{formation.entries.Sum(e => e.enemy.points):0000}";
            formation.id = index.ToString(); // FIXME I'll need to keep them stable in release 
        }

        public void UpdatePack() => packEditor?.UpdatePack();

        private void OnDrawGizmosSelected()
        {
            DrawBorderRect();
            DrawFormationEntries();
        }

        private void DrawFormationEntries()
        {
            for (int i = 0; i < formation.entries.Length; i++)
            {
                DrawFormationEntry(i);
            }
        }

        private void DrawFormationEntry(int index)
        {
            var formationEntry = formation.entries[index];
            
            if (formationEntry.enemy == null)
            {
                Debug.LogWarning($"No stats for {gameObject.name}:{index}");
                return;
            }
            
            Gizmos.DrawIcon(formationEntry.pos, AssetDatabase.GetAssetPath(formationEntry.enemy.gizmoIcon), true);
        }

        private static void DrawBorderRect()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Vector3.zero, 10 * Vector3.one);
        }
#endif
    }
}
