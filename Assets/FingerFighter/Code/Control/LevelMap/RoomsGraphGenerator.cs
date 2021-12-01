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
        [Tooltip("Indexes of connected rooms")]
        [SerializeField] private List<Vector2Int> connections;

        private static readonly Random Rand = new Random();
        
        private void OnDrawGizmos()
        {
            // Connections
            foreach (var connection in connections)
            {
                Gizmos.DrawLine(rooms[connection.x].pos, rooms[connection.y].pos);
            }
            
            // Marks
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
            SortRoomsByGridY();
            GenerateConnections();
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

        private void SortRoomsByGridY()
        {
            rooms = rooms.OrderBy(room => room.gridPos.y).ToList();
        }

        private void GenerateConnections()
        {
            var connectionsSet = new HashSet<Vector2Int>();
            for (int i = 0; i < rooms.Count - 1; i++)
            {
                connectionsSet.Add(ConnectionToNextRoom(i));
            }
            // TODO add previous connections 
            connections = connectionsSet.ToList();
        }

        private Vector2Int ConnectionToNextRoom(int roomIndex)
        {
            var room = rooms[roomIndex];
            var bestDistance = float.MaxValue;
            var bestNextRoomIndex = rooms.Count - 1;
            for (int i = roomIndex + 1; i < rooms.Count; i++)
            {
                if(rooms[i].gridPos.y == room.gridPos.y) continue;
                var nextDistance = Vector2Int.Distance(room.gridPos, rooms[i].gridPos);
                if (nextDistance < bestDistance)
                {
                    bestDistance = nextDistance;
                    bestNextRoomIndex = i;
                }
            }
            return new Vector2Int(roomIndex, bestNextRoomIndex);
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
