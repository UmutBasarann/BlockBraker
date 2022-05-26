using System;
using BlockBraker.Scripts.Config.GameConfig;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BlockBraker.Scripts.Managers.IntroSceneManager
{
    public class IntroSceneManager : MonoBehaviour
    {
        #region SerializeField

        [SerializeField] private Button btnStart = null;

        #endregion

        #region Awake | Start | Update

        private void Start()
        {
            AddEvents();
        }

        #endregion

        #region Load: GameScene

        private void LoadGameScene()
        {
            SceneManager.LoadScene(GameConfig.GAME_SCENE_ID);
        }

        #endregion

        #region Event: OnBtnStartClicked

        private void OnBtnStartClicked()
        {
            LoadGameScene();
            
            RemoveEvents();
        }

        #endregion

        #region AddEvents

        private void AddEvents()
        {
            btnStart.onClick.AddListener(OnBtnStartClicked);
        }

        #endregion

        #region RemoveEvents

        private void RemoveEvents()
        {
            btnStart.onClick.RemoveAllListeners();
        }

        #endregion
    }
}

