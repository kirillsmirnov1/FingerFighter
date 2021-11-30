using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace FingerFighter.Control.LevelMap
{
    public class RoomsGraphGenerator : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Vector2Int roomCount = new Vector2Int(6, 10);
        [SerializeField] private Vector2Int minPos = new Vector2Int(-2, -3);
        [SerializeField] private Vector2Int maxPos = new Vector2Int(2, 3);
        
        [Header("Results")]
        [SerializeField] private List<Room> rooms;

        private static readonly Random Rand = new Random();
        
        private void OnDrawGizmos()
        {
            foreach (var room in rooms)
            {
                Gizmos.DrawSphere(room.pos, 0.1f);   
            }
        }

        public void Generate()
        {
            rooms = new List<Room>
            {
                new Room {pos = new Vector2(0, -4)}, // Starter 
                new Room {pos = new Vector2(0, 4)} // Boss
            };
            
            var roomsToAdd = Rand.Next(roomCount.x, roomCount.y + 1);
            var newRoom = rooms[0];
            for (var i = 0; i < roomsToAdd; i++)
            {
                while (rooms.Contains(newRoom))
                {
                    newRoom = new Room {
                        pos = new Vector2(
                            Rand.Next(minPos.x, maxPos.x + 1),
                            Rand.Next(minPos.y, maxPos.y + 1)) // TODO randomize 
                    };
                } 
                rooms.Add(newRoom);
            }
        }
    }

    [Serializable]
    public struct Room
    {
        public Vector2 pos;
    }
}
