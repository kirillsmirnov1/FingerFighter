using System;
using System.Collections.Generic;
using UnityEngine;

namespace FingerFighter.Model.LevelMaps
{
    [Serializable]
    public class Room
    {
        public Vector2Int gridPos;
        public Vector2 pos;
        public List<int> neighbours = new List<int>();
    }
}