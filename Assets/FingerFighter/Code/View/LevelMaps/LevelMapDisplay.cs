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
        [SerializeField] private Camera cam;
        
        private void Start()
        {
            SpawnMap();
        }

        public void SpawnMap()
        {
            ClearMap();
            SpawnRoomMarks();
            SpawnConnections();
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
            for (var i = 0; i < levelMapVariable.Value.rooms.Count; i++)
            {
                var room = levelMapVariable.Value.rooms[i];
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
            // TODO crete RoomConnectionView or smth like that
            // TODO set line sprite depending on proximity to player's mark
            foreach (var connection in levelMapVariable.Value.connections)
            {
                var lineRenderer = Instantiate(connectionPrefab, transform).GetComponent<LineRenderer>();
                var v3Positions = levelMapVariable.Value.ConnectionPositions(connection).Select(pos => (Vector3) pos)
                    .ToArray();
                lineRenderer.SetPositions(v3Positions);
            }
        }
    }
}