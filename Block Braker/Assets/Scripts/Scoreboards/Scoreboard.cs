using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace BlockBraker.Scripts.Scoreboards
{
    public class Scoreboard : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private int maxScoreboardEntries = 5;
        [SerializeField] private Transform highScoresHolderTransform = null;
        [SerializeField] private GameObject scoreboardEntryObject = null;
        [SerializeField] private Button btnClose = null;

        [Header("Test")]
        [SerializeField] private int testEntryScore = 0;

        #endregion

        private static Scoreboard _instance;
        public static Scoreboard Instance => _instance;
        
        #region Fields

        private string SavePath => $"{Application.persistentDataPath}/highScores.json";

        #endregion

        #region Awake | Start | Update

        private void Awake()
        {
            if (_instance is null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(_instance.gameObject);
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            ScoreboardSaveData savedScores = GetSavedScores();

            UpdateUI(savedScores);

            SaveScores(savedScores);
        }

        #endregion

        #region OnEnable

        private void OnEnable()
        {
            AddEvents();
        }

        #endregion

        #region OnDisable

        private void OnDisable()
        {
            RemoveEvents();
        }

        #endregion

        #region Add: Entry
        
        [ContextMenu("AddTestEntry")]
        public void AddTestEntry()
        {
            AddEntry(new ScoreboardEntryData()
            {
                entryScore = testEntryScore
            });
        }

        public void AddEntry(ScoreboardEntryData scoreboardEntryData)
        {
            ScoreboardSaveData savedScores = GetSavedScores();

            bool scoreAdded = false;

            for (int i = 0; i < savedScores.highScores.Count; i++)
            {
                if (testEntryScore > savedScores.highScores[i].entryScore)
                {
                    savedScores.highScores.Insert(i, scoreboardEntryData);
                    scoreAdded = true;
                    break;
                }
            }

            if (!scoreAdded && savedScores.highScores.Count < maxScoreboardEntries)
            {
                savedScores.highScores.Add(scoreboardEntryData);
            }

            if (savedScores.highScores.Count > maxScoreboardEntries)
            {
                savedScores.highScores.RemoveRange(maxScoreboardEntries, savedScores.highScores.Count - maxScoreboardEntries);
            }

            UpdateUI(savedScores);

            SaveScores(savedScores);
        }
        
        #endregion

        #region UpdateUI

        private void UpdateUI(ScoreboardSaveData savedScores)
        {
            foreach (Transform child in highScoresHolderTransform)
            {
                Destroy(child.gameObject);
            }

            foreach (ScoreboardEntryData highScore in savedScores.highScores)
            {
                Instantiate(scoreboardEntryObject, highScoresHolderTransform).GetComponent<ScoreboardEntryUI>().Init(highScore);
            }
        }

        #endregion
        
        #region Get: SavedScores

        private ScoreboardSaveData GetSavedScores()
        {
            if (!File.Exists(SavePath))
            {
                File.Create(SavePath).Dispose();
                return new ScoreboardSaveData();
            }

            using (StreamReader stream = new StreamReader(SavePath))
            {
                string json = stream.ReadToEnd();

                return JsonUtility.FromJson<ScoreboardSaveData>(json);
            }
        }

        #endregion

        #region SaveScores

        private void SaveScores(ScoreboardSaveData scoreboardSaveData)
        {
            using (StreamWriter stream = new StreamWriter(SavePath))
            {
                string json = JsonUtility.ToJson(scoreboardSaveData, true);
                stream.Write(json);
            }
        }

        #endregion

        #region Event: OnBtnCloseClicked

        private void OnBtnCloseClicked()
        {
            gameObject.SetActive(false);
        }

        #endregion

        #region AddEvents

        private void AddEvents()
        {
            btnClose.onClick.AddListener(OnBtnCloseClicked);
        }

        #endregion
        
        #region RemoveEvents

        private void RemoveEvents()
        {
            btnClose.onClick.RemoveAllListeners();
        }

        #endregion
    }
}


