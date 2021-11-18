using UnityEngine;

namespace FingerFighter.Control.Conditions.Flags
{
    public abstract class AFlag : MonoBehaviour
    {
        public bool On { get; protected set; }
    }
}