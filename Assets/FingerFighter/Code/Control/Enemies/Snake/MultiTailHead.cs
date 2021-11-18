using UnityEngine;

namespace FingerFighter.Control.Enemies.Snake
{
    public class MultiTailHead : MonoBehaviour
    {
        [SerializeField] private GameObject headSegment;

        // TODO lock health until tails are dead
        // TODO position subheads on enable

        private void OnEnable()
        {
            headSegment.gameObject.SetActive(true);
        }
    }
}
