using UnityEngine;

namespace FingerFighter.View.Display.SceneChangeHex
{
    public class HexGrid : MonoBehaviour
    {
        [SerializeField][Range(0f, 1f)] public float t;
        [Tooltip("Gap between rows scaling")]
        [SerializeField] private float delay = 0.01f;
        [Tooltip("Relative time it takes for row to scale")]
        [SerializeField] private float duration = 0.1f;
        
        [HideInInspector][SerializeField] private float tMod;
        [HideInInspector][SerializeField] private HexRow[] rows;
        [HideInInspector][SerializeField] private float[] rowsDelay;
        
        private void OnValidate()
        {
            rows = GetComponentsInChildren<HexRow>();
            tMod = rows.Length * delay + duration;
            InitRowsDelay();
            Scale(t);
        }

        private void InitRowsDelay()
        {
            rowsDelay = new float[rows.Length];
            for (int i = 0; i < rowsDelay.Length; i++)
            {
                rowsDelay[i] = i * delay;
            }
        }

        public void Scale(float scale)
        {
            for (int i = 0; i < rows.Length; i++)
            {
                var index = rows.Length - 1 - i;
                rows[index].Scale(Mathf.InverseLerp(rowsDelay[i], rowsDelay[i] + duration, scale * tMod));
            }
        }
    }
}
