using UnityEngine;

namespace FingerFighter.Control.Common.Enemies.Snake
{
    public class FollowSnakeHead : MonoBehaviour
    {
        [SerializeField] private SnakeHead snakeHead;
        
        private void Update()
        {
            transform.position = snakeHead.HeadSegment.position;
        }
    }
}