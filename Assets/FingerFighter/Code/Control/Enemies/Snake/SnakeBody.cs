using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Control.Enemies.Snake
{
    public class SnakeBody : MonoBehaviour
    {
        [SerializeField] private TransformVariable player;
        public Transform Player => player.Value;
    }
}