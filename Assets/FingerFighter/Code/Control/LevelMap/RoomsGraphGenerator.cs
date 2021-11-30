using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityUtils;
using Random = System.Random;

namespace FingerFighter.Control.LevelMap
{
    public class RoomsGraphGenerator : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Vector2Int roomCount = new Vector2Int(6, 10);
        [SerializeField] private Vector2Int minPos = new Vector2Int(-2, -3);
        [SerializeField] private Vector2Int maxPos = new Vector2Int(2, 3);
        [SerializeField] private float roomMarkRadius = 0.2f;
        [SerializeField] private float shiftMaxRadius = 0.25f;
        
        [Header("Results")]
        [SerializeField] private List<Room> rooms;

        private static readonly Random Rand = new Random();
        
        private void OnDrawGizmos()
        {
            foreach (var room in rooms)
            {
                // Gizmos.DrawSphere((Vector3Int)room.gridPos, roomMarkRadius);   
                Gizmos.DrawSphere(room.pos, roomMarkRadius);   
            }
        }

        public void Generate()
        {
            SetAnchorRooms();
            GenerateMiddleRooms();
        }

        private void SetAnchorRooms()
        {
            var top = new Vector2Int(0, 4);
            rooms = new List<Room>
            {
                new Room {gridPos = -top, pos = -top}, // Start
                new Room {gridPos = top, pos = top} // End
            };
        }

        private void GenerateMiddleRooms()
        {
            var roomsToAdd = Rand.Next(roomCount.x, roomCount.y + 1);
            var positions = new HashSet<Vector2Int>(rooms.Select(r => r.gridPos));
            var newPos = rooms[0].gridPos;

            for (var i = 0; i < roomsToAdd; i++)
            {
                while (positions.Contains(newPos))
                {
                    newPos = new Vector2Int(
                        Rand.Next(minPos.x, maxPos.x + 1),
                        Rand.Next(minPos.y, maxPos.y + 1));
                }
                positions.Add(newPos);

                rooms.Add(new Room
                {
                    gridPos = newPos,
                    pos = newPos + NextShift,
                });
            }
        }

        private Vector2 NextShift =>
            new Vector2(
                Rand.NextFloat(-shiftMaxRadius, shiftMaxRadius),
                Rand.NextFloat(-shiftMaxRadius, shiftMaxRadius)
            );
    }

    [Serializable]
    public class Room
    {
        public Vector2Int gridPos;
        public Vector2 pos;
    }
}
