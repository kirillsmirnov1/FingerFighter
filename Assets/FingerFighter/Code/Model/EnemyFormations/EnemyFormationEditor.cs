using UnityEngine;

namespace FingerFighter.Model.EnemyFormations
{
    public class EnemyFormationEditor : MonoBehaviour
    {
        [SerializeField] public EnemyFormationEntry[] formationEntries;
        
        private void OnDrawGizmosSelected()
        {
            DrawBorderRect();
            DrawFormationEntries();
        }

        private void DrawFormationEntries()
        {
            for (int i = 0; i < formationEntries.Length; i++)
            {
                DrawFormationEntry(i);
            }
        }

        private void DrawFormationEntry(int index)
        {
            var formationEntry = formationEntries[index];
            
            if (formationEntry.enemy == null)
            {
                Debug.LogWarning($"No stats for {gameObject.name}:{index}");
                return;
            }

            Gizmos.color = formationEntry.enemy.gizmoColor;
            Gizmos.DrawSphere(formationEntry.pos, 0.3f);
        }

        private static void DrawBorderRect()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Vector3.zero, 10 * Vector3.one);
        }
    }
}