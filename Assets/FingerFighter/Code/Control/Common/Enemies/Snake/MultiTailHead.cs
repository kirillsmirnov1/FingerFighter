using UnityEngine;

namespace FingerFighter.Control.Common.Enemies.Snake
{
    public class MultiTailHead : MonoBehaviour
    {
        [SerializeField] private GameObject headSegment;
        [SerializeField] private SnakeHead[] subheads;

        private void OnEnable()
        {
            headSegment.gameObject.SetActive(true);
            foreach (var subHead in subheads)
            {
                subHead.InitTarget();
                subHead.MoveToTargetPos();
            }
        }
    }
}
