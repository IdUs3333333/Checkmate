using System.IO;
using UnityEngine;
using SaveSystem.Data;
using UnityEngine.SocialPlatforms.Impl;

namespace SaveSystem.Manager
{
    public class DataManager : MonoBehaviour
    {
        private string savePath;
        private GameData gameData = new GameData();

        private void Awake()
        {
            savePath = Path.Combine(Application.dataPath, "playerData.json");
        }
        private void Start()
        {
            LoadData();
        }

        public void SaveData()
        {
            string json = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(savePath, json);
        }

        public void LoadData()
        {
            if (File.Exists(savePath))
            {
                string json = File.ReadAllText(savePath);
                gameData = JsonUtility.FromJson<GameData>(json);
            }
            else
            {
                gameData = new GameData();
                SaveData();
            }
        }


        private DifficultyData this[Difficulty difficulty]
        {
            get
            {
                return difficulty switch
                {
                    Difficulty.Easy => gameData.easy,
                    Difficulty.Normal => gameData.normal,
                    Difficulty.Hard => gameData.hard,
                    _ => null,
                };
            }
            set
            {
                switch (difficulty)
                {
                    case Difficulty.Easy:
                        gameData.easy = value;
                        break;
                    case Difficulty.Normal:
                        gameData.normal = value;
                        break;
                    case Difficulty.Hard:
                        gameData.hard = value;
                        break;
                }
            }
        }

        #region 값 세팅
        public void SetHighScore(Difficulty difficulty, int score)
        {
            this[difficulty].highScore = score;
        }
        public void SetPlayTime(Difficulty difficulty, float time)
        {
            this[difficulty].shortest_Record = time;
        }
        #endregion

        #region 값 가져오기
        public int GetHighScore(Difficulty difficulty)
        {
            return this[difficulty]?.highScore ?? 0;
        }

        public float GetPlayTime(Difficulty difficulty)
        {
            return this[difficulty]?.shortest_Record ?? 0f;
        }
        #endregion


        //스코어 갱신
        public void TrySetHighScore(Difficulty difficulty, int score)
        {
            int currentHighScore = GetHighScore(difficulty);
            if (score > currentHighScore)
            {
                SetHighScore(difficulty, score);
                SaveData();
                Debug.Log($"스코어 갱신!!{currentHighScore} -> {score}");
            }
        }

        //클리어 시간 갱신
        public void TrySetPlayTime(Difficulty difficulty, float time)
        {
            float currentPlayTime = GetPlayTime(difficulty);
            if (currentPlayTime == 0f || time < currentPlayTime)
            {
                SetPlayTime(difficulty, time);
                SaveData();
                Debug.Log($"최단시간 갱신!!{currentPlayTime} -> {time}");
            }
        }
    }
}