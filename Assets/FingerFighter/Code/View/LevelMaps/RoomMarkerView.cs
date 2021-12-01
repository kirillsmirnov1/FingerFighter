using FingerFighter.Model.LevelMaps;
using UnityEngine;

namespace FingerFighter.View.LevelMaps
{
    [RequireComponent(typeof(RectTransform))]
    public class RoomMarkerView : MonoBehaviour
    {
        [SerializeField] private RectTransform rect;

        private RoomMarkerData _data;
        
        // TODO set button interaction depending on proximity to player
        // TODO notify on click
        
        private void OnValidate()
        {
            rect ??= GetComponent<RectTransform>();
        }

        public void Init(RoomMarkerData data)
        {
            _data = data;
            rect.position = _data.position;
        }
    }
}