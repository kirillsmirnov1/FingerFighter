using FingerFighter.Model.LevelMaps;
using UnityEngine;

namespace FingerFighter.Control.Ring
{
    public class RingFlow : MonoBehaviour
    {
        [SerializeField] private LevelMapVariable levelMapVariable;

        private void Awake()
        {
            levelMapVariable.Value = null; // For ring loaded only when level is over
        }
    }
}