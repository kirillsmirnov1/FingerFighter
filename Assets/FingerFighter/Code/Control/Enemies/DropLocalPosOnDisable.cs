using UnityEngine;

namespace FingerFighter.Control.Enemies
{
    // Used to make multi-element enemies respawn in correct position 
    public class DropLocalPosOnDisable : MonoBehaviour
    {
        private void OnDisable()
        {
            transform.localPosition = Vector3.zero;
        }
    }
}
