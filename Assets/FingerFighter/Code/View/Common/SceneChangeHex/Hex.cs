using UnityEngine;

namespace FingerFighter.View.Common.SceneChangeHex
{
    public class Hex : MonoBehaviour
    {
        [SerializeField] private Vector3 one = Vector3.one;

        public void Scale(float scale)
            => transform.localScale = scale * one;
    }
}