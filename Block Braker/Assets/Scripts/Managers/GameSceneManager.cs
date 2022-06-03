using System;
using BlockBraker.Scripts.Config.GameConfig;
using BlockBraker.Scripts.Scoreboards;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BlockBraker.Scripts.Managers
{
    public class GameSceneManager : MonoBehaviour
    {
        #region Event | Action

        public static event Action OnUpdate;

        #endregion
        
        #region SerializeField

        [SerializeField] private Scoreboard scoreboard = null;
        [SerializeField] private Button btnStart = null;
        [SerializeField] private Button btnHighScore = null;

        #endregion

        #region Singleton

        private static GameSceneManager _instance;
        public static GameSceneManager Instance => _instance;

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

        public void Start()
        {
            AddEvents();
        }

        private void Update()
        {
            if (OnUpdate != null)
            {
                OnUpdate();
            }
        }

        #endregion

        #region Load: GameScene

        public void LoadGameScene()
        {
            SceneManager.LoadScene(GameConfig.GAME_SCENE_ID);
        }

        #endregion

        #region Load: IntroScene

        public void LoadIntroScene()
        {
            SceneManager.LoadScene(GameConfig.INTRO_SCENE_ID);
        }

        #endregion

        #region Event: OnBtnStartClicked

        private void OnBtnStartClicked()
        {
            LoadGameScene();
            
            RemoveEvents();
        }

        #endregion

        #region Event: OnBtnHighScoreClicked

        private void OnBtnHighScoreClicked()
        {
            scoreboard.gameObject.SetActive(true);
        }

        #endregion

        #region AddEvents

        private void AddEvents()
        {
            btnStart.onClick.AddListener(OnBtnStartClicked);
            btnHighScore.onClick.AddListener(OnBtnHighScoreClicked);
        }

        #endregion

        #region RemoveEvents

        private void RemoveEvents()
        {
            btnStart.onClick.RemoveAllListeners();
            btnHighScore.onClick.RemoveAllListeners();
        }

        #endregion
    }
}

