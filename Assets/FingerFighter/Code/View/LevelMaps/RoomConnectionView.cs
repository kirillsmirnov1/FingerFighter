using FingerFighter.Model.LevelMaps;
using FingerFighter.View.LevelMaps.Player;
using UnityEngine;
using UnityUtils;
using UnityUtils.Variables;

namespace FingerFighter.View.LevelMaps
{
    [RequireComponent(typeof(LineRenderer))]
    public class RoomConnectionView : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private IntVariable currentRoom;
        [SerializeField] private Material availableMat;
        [SerializeField] private Material unAvailableMat;
        
        private RoomConnectionData _data;
        private Vector2Int Connection => _data.connection;

        private void OnValidate()
        {
            lineRenderer ??= GetComponent<LineRenderer>();
            this.CheckNullFields();
        }

        private void Awake()
        {
            PlayerMarker.OnRoomReached += SetLineMaterial;
        }

        private void OnDestroy()
        {
            PlayerMarker.OnRoomReached -= SetLineMaterial;
        }

        public void Init(RoomConnectionData data)
        {
            _data = data;
            lineRenderer.SetPositions(_data.positions);
            SetLineMaterial(currentRoom);
        }

        private void SetLineMaterial(int currentRoomIndex)
        {
            var availableConnection = currentRoomIndex == Connection.x || currentRoomIndex == Connection.y;
            lineRenderer.material = availableConnection ? availableMat : unAvailableMat;
        }
    }
}