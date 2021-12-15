using UnityEngine;

namespace FingerFighter.View.Common.SceneChangeHex
{
    public class HexRow : MonoBehaviour
    {
        [SerializeField] private Hex[] hexes;

        private void OnValidate() 
            => hexes = GetComponentsInChildren<Hex>();

        public void Scale(float scale)
        {
            for (int i = 0; i < hexes.Length; i++)
            {
                hexes[i].Scale(scale);
            }
        }
    }
}