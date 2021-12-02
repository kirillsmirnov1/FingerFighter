using System.Collections.Generic;
using UnityEngine;

namespace FingerFighter.Model.LevelMaps
{
    public struct RoomMarkerData
    {
        public RoomType type;
        public float difficulty;
        public Vector2 position;
        public int roomIndex;
        public HashSet<int> neighbours;
    }
}