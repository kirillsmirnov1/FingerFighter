using System.Collections.Generic;
using System.Linq;
using FingerFighter.Model.LevelMaps;
using UnityEngine;

namespace FingerFighter.View.LevelMaps
{
    public class LevelMapDisplay : MonoBehaviour
    {
        [SerializeField] private LevelMapVariable levelMapVariable;
        [SerializeField] private GameObject roomMarkPrefab;
        [SerializeField] private GameObject connectionPrefab;

        [Header("Game objects")]
        [SerializeField] private RectTransform cancelButton;
        [SerializeField] private RectTransform continueButton;
        [SerializeField] private Camera cam;

        private List<Room> Rooms 
            => levelMapVariable.Value.rooms;

        private void Start()
        {
            SpawnMap();
        }

        public void SpawnMap()
        {
            ClearMap();
            SpawnRoomMarks();
            SpawnConnections();
            PositionButtons();
        }

        public void ClearMap()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        private void SpawnRoomMarks()
        {
            for (var i = 0; i < Rooms.Count; i++)
            {
                var room = Rooms[i];
                Instantiate(roomMarkPrefab, transform)
                    .GetComponent<RoomMarkerView>()
                    .Init(new RoomMarkerData
                    {
                        type = room.type,
                        difficulty = room.difficulty,
                        position = cam.WorldToScreenPoint(room.pos),
                        roomIndex = i,
                        neighbours = new HashSet<int>(room.neighbours),
                    });
            }
        }

        private void SpawnConnections()
        {
            foreach (var connection in levelMapVariable.Value.connections)
            {
                Instantiate(connectionPrefab, transform)
                    .GetComponent<RoomConnectionView>()
                    .Init(new RoomConnectionData
                    {
                        connection = connection,
                        positions = levelMapVariable.Value
                            .ConnectionPositions(connection)
                            .Select(pos => (Vector3) pos)
                            .ToArray() 
                    });
            }
        }

        private void PositionButtons()
        {
            continueButton.position = cam.WorldToScreenPoint(Rooms[Rooms.Count-1].pos + Vector2.up);
            cancelButton.position = cam.WorldToScreenPoint(Rooms[0].pos + Vector2.down);
        }
    }
}