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

            var stats = formationEntry.enemy;

            
            if (stats.gizmoTexture == null)
            {
                Gizmos.color = stats.gizmoColor;
                Gizmos.DrawSphere(formationEntry.pos, 0.3f);
            }
            else
            {
                var rectSize = Vector2.one;  
                var reqPos = formationEntry.pos;
                var rectPos = new Vector2(reqPos.x - rectSize.x / 2f, reqPos.y - rectSize.y / 2f);  
                var rect = new Rect(rectPos, rectSize);
                // FIXME Unity messes scale up
                // FIXME so position is wrong to
                // FIXME need to draw with another method 
                Gizmos.DrawGUITexture(rect, stats.gizmoTexture); 
            }
        }

        private static void DrawBorderRect()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Vector3.zero, 10 * Vector3.one);
        }
    }
}