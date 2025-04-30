using UnityEngine;
using System.IO;
using System;


namespace SaveSystem.Data
{
    [Serializable]
    public class DifficultyData
    {
        public int highScore;
        public float shortest_Record;
    }

    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }
}