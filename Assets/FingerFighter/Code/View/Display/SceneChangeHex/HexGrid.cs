using UnityEngine;

namespace FingerFighter.View.Display.SceneChangeHex
{
    public class HexGrid : MonoBehaviour
    {
        [SerializeField][Range(0f, 1f)] private float t;
        [SerializeField] private float delay = 0.01f;
        [SerializeField] private float k = 5;
        [SerializeField] private float totalDelay;
        [SerializeField] private HexRow[] rows;
        
        private void OnValidate()
        {
            rows = GetComponentsInChildren<HexRow>();
            totalDelay = delay * (rows.Length + k);
            Scale(t);
        }

        public void Scale(float scale)
        {
            for (int i = 0; i < rows.Length; i++)
            {
                rows[i].Scale(Mathf.Clamp01(scale * totalDelay - delay * (rows.Length - i - 1)));
            }
        }
    }
}
