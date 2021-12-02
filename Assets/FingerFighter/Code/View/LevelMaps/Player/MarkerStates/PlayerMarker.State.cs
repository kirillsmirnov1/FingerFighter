using UnityEngine;

namespace FingerFighter.View.LevelMaps.Player
{
    public partial class PlayerMarker
    {
        private abstract class State
        {
            protected readonly PlayerMarker Marker;
            protected readonly Transform Self;
            protected State(PlayerMarker marker)
            {
                Marker = marker;
                Self = Marker.transform;
            }

            public abstract void OnRoomMarkerClicked(int roomIndex);

            public abstract void OnUpdate();
        }
    }
}