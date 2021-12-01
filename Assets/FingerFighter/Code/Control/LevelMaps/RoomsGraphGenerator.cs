using System.Collections.Generic;
using System.Linq;
using FingerFighter.Model.LevelMaps;
using UnityEngine;
using UnityUtils;
using Random = System.Random;

namespace FingerFighter.Control.LevelMaps
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
        [SerializeField] private LevelMapVariable levelMapVariable;
        
        private static readonly Random Rand = new Random();
        private LevelMap levelMap => levelMapVariable.Value;
        
        private void OnDrawGizmos()
        {
            if(levelMapVariable == null || levelMap == null) return;
            
            // Connections
            foreach (var connection in levelMap.connections)
            {
                Gizmos.DrawLine(levelMap.rooms[connection.x].pos, levelMap.rooms[connection.y].pos);
            }
            
            // Marks
            foreach (var room in levelMap.rooms)
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
            levelMap.rooms = new List<Room>
            {
                new Room {gridPos = -top, pos = -top}, // Start
                new Room {gridPos = top, pos = top} // End
            };
        }

        private void GenerateMiddleRooms()
        {
            var roomsToAdd = Rand.Next(roomCount.x, roomCount.y + 1);
            var positions = new HashSet<Vector2Int>(levelMap.rooms.Select(r => r.gridPos));
            var newPos = levelMap.rooms[0].gridPos;

            for (var i = 0; i < roomsToAdd; i++)
            {
                while (positions.Contains(newPos))
                {
                    newPos = new Vector2Int(
                        Rand.Next(minPos.x, maxPos.x + 1),
                        Rand.Next(minPos.y, maxPos.y + 1));
                }
                positions.Add(newPos);

                levelMap.rooms.Add(new Room
                {
                    gridPos = newPos,
                    pos = newPos + NextShift,
                });
            }
        }

        private void SortRoomsByGridY()
        {
            levelMap.rooms = levelMap.rooms.OrderBy(room => room.gridPos.y).ToList();
        }

        private void GenerateConnections()
        {
            var connectionsSet = new HashSet<Vector2Int>();
            for (int i = 0; i < levelMap.rooms.Count - 1; i++)
            {
                connectionsSet.Add(ConnectionToNextRoom(i));
            }
            for (int i = 1; i < levelMap.rooms.Count; i++)
            {
                connectionsSet.Add(ConnectionToPrevRoom(i));   
            }
            levelMap.connections = connectionsSet.ToList();
        }

        private Vector2Int ConnectionToNextRoom(int roomIndex) 
            => new Vector2Int(roomIndex, ClosestRoomIndex(roomIndex, roomIndex + 1, levelMap.rooms.Count));

        private Vector2Int ConnectionToPrevRoom(int roomIndex) 
            => new Vector2Int(ClosestRoomIndex(roomIndex, 0, roomIndex), roomIndex);

        private int ClosestRoomIndex(int roomIndex, int from, int toExclusive)
        {
            var room = levelMap.rooms[roomIndex];
            var bestDistance = float.MaxValue;
            var bestNextRoomIndex = levelMap.rooms.Count - 1;
            for (int i = from; i < toExclusive; i++)
            {
                if(levelMap.rooms[i].gridPos.y == room.gridPos.y) continue;
                var nextDistance = Vector2Int.Distance(room.gridPos, levelMap.rooms[i].gridPos);
                if (nextDistance < bestDistance)
                {
                    bestDistance = nextDistance;
                    bestNextRoomIndex = i;
                }
            }

            return bestNextRoomIndex;
        }

        private Vector2 NextShift =>
            new Vector2(
                Rand.NextFloat(-shiftMaxRadius, shiftMaxRadius),
                Rand.NextFloat(-shiftMaxRadius, shiftMaxRadius)
            );
    }
}
