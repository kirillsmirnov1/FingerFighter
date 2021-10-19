using UnityEngine;

namespace FingerFighter.Model.EnemyFormations
{
    public class EnemyFormation : MonoBehaviour
    {
        [SerializeField] private EnemyFormationEntry[] formationEntries;
        
        private void OnDrawGizmosSelected()
        {
            DrawBorderRect();
            DrawFormationEntries();
        }

        private void DrawFormationEntries()
        {
            for (int i = 0; i < formationEntries.Length; i++)
            {
                DrawFormationEntry(formationEntries[i]);
            }
        }

        private void DrawFormationEntry(EnemyFormationEntry formationEntry)
        {
            Gizmos.DrawWireSphere(formationEntry.pos, 0.3f);
        }

        private static void DrawBorderRect()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Vector3.zero, 10 * Vector3.one);
        }
    }
}