using UnityEngine;

namespace FingerFighter.Model.EnemyFormations
{
    public class EnemyFormation : MonoBehaviour
    {
        private void OnDrawGizmosSelected()
        {
            DrawBorderRect();
        }

        private static void DrawBorderRect()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Vector3.zero, 10 * Vector3.one);
        }
    }
}