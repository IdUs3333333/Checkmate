using SaveSystem.Data;
using System;
using UnityEngine;

namespace SaveSystem.Data
{
    [Serializable]
    public class GameData
    {
        public DifficultyData easy = new DifficultyData();
        public DifficultyData normal = new DifficultyData();
        public DifficultyData hard = new DifficultyData();
    }
}