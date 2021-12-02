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
        [SerializeField] private float difficultyVariation = 0.1f;
        
        [Header("Results")] 
        [SerializeField] private LevelMapVariable levelMapVariable;

        [Header("Debug")]
        [SerializeField] private bool gizmoMarks = true;
        [SerializeField] private bool gizmoConnections = true;
        
        private static readonly Random Rand = new Random();
        private LevelMap _levelMap;  
        
        private void OnDrawGizmos()
        {
            if(levelMapVariable == null || _levelMap == null) return;

            if (gizmoConnections)
            {
                // Connections
                foreach (var connection in _levelMap.connections)
                {
                    var positions = _levelMap.ConnectionPositions(connection);
                    Gizmos.DrawLine(positions[0], positions[1]);
                }
            }

            if (gizmoMarks)
            {
                // Marks
                foreach (var room in _levelMap.rooms)
                {
                    // Gizmos.DrawSphere((Vector3Int)room.gridPos, roomMarkRadius);   
                    Gizmos.DrawSphere(room.pos, roomMarkRadius);   
                }
            }
        }

        public void Generate()
        {
            CreateNewMap();
            SetAnchorRooms();
            GenerateMiddleRooms();
            SortRoomsByGridY();
            GenerateConnections();
            WriteResults();
        }

        private void CreateNewMap()
        {
            _levelMap = new LevelMap();
        }

        private void SetAnchorRooms()
        {
            var top = new Vector2Int(0, 4);
            _levelMap.rooms = new List<Room>
            {
                new Room {type = RoomType.Start, gridPos = -top, pos = -top},
                new Room {type = RoomType.Boss, gridPos = top, pos = top}
            };
        }

        private void GenerateMiddleRooms()
        {
            var roomsToAdd = Rand.Next(roomCount.x, roomCount.y + 1);
            var positions = new HashSet<Vector2Int>(_levelMap.rooms.Select(r => r.gridPos));
            var newPos = _levelMap.rooms[0].gridPos;

            for (var i = 0; i < roomsToAdd; i++)
            {
                while (positions.Contains(newPos))
                {
                    newPos = new Vector2Int(
                        Rand.Next(minPos.x, maxPos.x + 1),
                        Rand.Next(minPos.y, maxPos.y + 1));
                }
                positions.Add(newPos);
                
                var shiftedPos = newPos + NextShift;
                var difficulty = GenerateRoomDifficulty(shiftedPos.y);

                _levelMap.rooms.Add(new Room
                {
                    type = RoomType.Regular,
                    difficulty = difficulty,
                    gridPos = newPos,
                    pos = shiftedPos,
                });
            }
        }

        private float GenerateRoomDifficulty(float yPos)
        {
            var t = Mathf.InverseLerp(minPos.y, maxPos.y, yPos);
            var difficulty = t + Rand.NextFloat(-difficultyVariation, difficultyVariation);
            return Mathf.Clamp01(difficulty);
        }

        private void SortRoomsByGridY()
        {
            _levelMap.rooms = _levelMap.rooms.OrderBy(room => room.gridPos.y).ToList();
        }

        private void GenerateConnections()
        {
            var connectionsSet = new HashSet<Vector2Int>();
            for (int i = 0; i < _levelMap.rooms.Count - 1; i++)
            {
                connectionsSet.Add(ConnectionToNextRoom(i));
            }
            for (int i = 1; i < _levelMap.rooms.Count; i++)
            {
                connectionsSet.Add(ConnectionToPrevRoom(i));   
            }
            _levelMap.connections = connectionsSet.ToList();
            for (var i = 0; i < _levelMap.connections.Count; i++)
            {
                var connection = _levelMap.connections[i];
                _levelMap.rooms[connection.x].neighbours.Add(connection.y);
                _levelMap.rooms[connection.y].neighbours.Add(connection.x);
            }
        }

        private Vector2Int ConnectionToNextRoom(int roomIndex) 
            => new Vector2Int(roomIndex, ClosestRoomIndex(roomIndex, roomIndex + 1, _levelMap.rooms.Count));

        private Vector2Int ConnectionToPrevRoom(int roomIndex) 
            => new Vector2Int(ClosestRoomIndex(roomIndex, 0, roomIndex), roomIndex);

        private int ClosestRoomIndex(int roomIndex, int from, int toExclusive)
        {
            var room = _levelMap.rooms[roomIndex];
            var bestDistance = float.MaxValue;
            var bestNextRoomIndex = _levelMap.rooms.Count - 1;
            for (int i = from; i < toExclusive; i++)
            {
                if(_levelMap.rooms[i].gridPos.y == room.gridPos.y) continue;
                var nextDistance = Vector2Int.Distance(room.gridPos, _levelMap.rooms[i].gridPos);
                if (nextDistance < bestDistance)
                {
                    bestDistance = nextDistance;
                    bestNextRoomIndex = i;
                }
            }

            return bestNextRoomIndex;
        }

        private void WriteResults()
        {
            levelMapVariable.Value = _levelMap;
        }

        private Vector2 NextShift =>
            new Vector2(
                Rand.NextFloat(-shiftMaxRadius, shiftMaxRadius),
                Rand.NextFloat(-shiftMaxRadius, shiftMaxRadius)
            );
    }
}
