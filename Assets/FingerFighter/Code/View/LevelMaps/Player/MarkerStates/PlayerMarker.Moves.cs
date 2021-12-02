using UnityEngine;

namespace FingerFighter.View.LevelMaps.Player
{
    public partial class PlayerMarker
    {
        private class Moves : State
        {
            private readonly Vector3 _targetPos;
            private readonly int _targetRoom;
            
            public Moves(PlayerMarker marker, int roomIndex) : base(marker)
            {
                _targetRoom = roomIndex;
                _targetPos = Marker.RoomMarkerScreenPosition(roomIndex);
            }
            
            public override void OnRoomMarkerClicked(int roomIndex)
            {
                // Does nothing 
            }

            public override void OnUpdate()
            {
                MoveToTarget();
            }
            
            private void MoveToTarget()
            {
                if (Vector3.Distance(Self.position, _targetPos) > Marker.eps)
                {
                    var toTarget = _targetPos - Self.position;
                    var step = toTarget.normalized * (Time.deltaTime * Marker.speed);
                    var nextMove = Vector3.ClampMagnitude(step, toTarget.magnitude);
                    Self.position += nextMove;
                }
                else
                {
                    RoomReached();
                }
            }
            
            private void RoomReached() 
                => Marker._state = new AtPosition(Marker, _targetRoom);
        }
    }
}