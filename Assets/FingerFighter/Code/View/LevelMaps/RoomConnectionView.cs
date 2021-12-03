using FingerFighter.Model.LevelMaps;
using UnityEngine;

namespace FingerFighter.View.LevelMaps
{
    [RequireComponent(typeof(LineRenderer))]
    public class RoomConnectionView : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;

        private RoomConnectionData _data;
        // TODO set line sprite depending on proximity to player's mark

        private void OnValidate()
        {
            lineRenderer ??= GetComponent<LineRenderer>();
        }

        public void Init(RoomConnectionData data)
        {
            _data = data;
            lineRenderer.SetPositions(_data.positions);
        }
    }
}