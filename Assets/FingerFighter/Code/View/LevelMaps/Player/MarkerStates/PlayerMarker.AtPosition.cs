namespace FingerFighter.View.LevelMaps.Player
{
    public partial class PlayerMarker
    {
        private class AtPosition : State
        {
            public AtPosition(PlayerMarker marker, int roomIndex) : base(marker)
            {
                Self.position = Marker.RoomMarkerScreenPosition(roomIndex);
            }
            
            public override void OnRoomMarkerClicked(int roomIndex)
            {
                Marker._state = new Moves(Marker, roomIndex);
            }

            public override void OnUpdate()
            {
                // Does nothing
            }
        }
    }
}