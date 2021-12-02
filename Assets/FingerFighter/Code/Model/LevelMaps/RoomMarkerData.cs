using System.Collections.Generic;
using UnityEngine;

namespace FingerFighter.Model.LevelMaps
{
    public struct RoomMarkerData
    {
        public Vector2 position;
        public int roomIndex;
        public HashSet<int> neighbours;
    }
}