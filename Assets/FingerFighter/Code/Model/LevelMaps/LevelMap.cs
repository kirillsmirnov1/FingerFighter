using System;
using System.Collections.Generic;
using UnityEngine;

namespace FingerFighter.Model.LevelMaps
{
    [Serializable]
    public class LevelMap
    {
        public List<Room> rooms;
        [Tooltip("Indexes of connected rooms")]
        public List<Vector2Int> connections;
    }
}