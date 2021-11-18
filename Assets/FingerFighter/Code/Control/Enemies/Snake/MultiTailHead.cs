using UnityEngine;

namespace FingerFighter.Control.Enemies.Snake
{
    public class MultiTailHead : MonoBehaviour
    {
        [SerializeField] private GameObject headSegment;
        [SerializeField] private SnakeHead[] subheads;
        
        // TODO lock health until tails are dead
        
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
