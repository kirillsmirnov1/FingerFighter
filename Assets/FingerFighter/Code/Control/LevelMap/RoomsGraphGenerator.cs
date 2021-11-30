using System;
using UnityEngine;

namespace FingerFighter.Control.LevelMap
{
    public class RoomsGraphGenerator : MonoBehaviour
    {
        [SerializeField] private Room[] rooms;
        
        private void OnDrawGizmos()
        {
            foreach (var room in rooms)
            {
                Gizmos.DrawSphere((Vector3Int) room.gridPos, 0.1f);   
            }
        }
    }

    [Serializable]
    public struct Room
    {
        public Vector2Int gridPos;
    }
}
