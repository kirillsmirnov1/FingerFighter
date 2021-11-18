using UnityEngine;

namespace FingerFighter.Control.Enemies.Snake
{
    public class MultiTailHead : MonoBehaviour
    {
        [SerializeField] private GameObject headSegment;

        // TODO lock health until tails are dead
        // TODO position subheads on enable
        // TODO move head segment towards player 

        private void OnEnable()
        {
            headSegment.gameObject.SetActive(true);
        }
    }
}
