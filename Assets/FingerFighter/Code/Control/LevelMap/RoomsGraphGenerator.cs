using System;
using System.Collections.Generic;
using UnityEngine;

namespace FingerFighter.Control.LevelMap
{
    public class RoomsGraphGenerator : MonoBehaviour
    {
        [SerializeField] private List<Room> rooms;
        
        private void OnDrawGizmos()
        {
            foreach (var room in rooms)
            {
                Gizmos.DrawSphere((Vector3Int) room.gridPos, 0.1f);   
            }
        }

        public void Generate()
        {
            rooms = new List<Room>
            {
                new Room {gridPos = new Vector2Int(0, -4)}, // Starter 
                new Room {gridPos = new Vector2Int(0, 4)} // Boss
            };
            
            // TODO add more
        }
    }

    [Serializable]
    public struct Room
    {
        public Vector2Int gridPos;
    }
}
