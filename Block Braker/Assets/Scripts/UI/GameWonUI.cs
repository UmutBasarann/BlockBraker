using BlockBraker.Scripts.Managers;
using BlockBraker.Scripts.Managers.GameManager;
using UnityEngine;
using UnityEngine.UI;

namespace BlockBraker.Scripts.UI
{
    public class GameWonUI : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private Button btnGameWon = null;
        [SerializeField] private Button btnExit = null;

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

        #region Event: OnBtnGameWonClicked

        private void OnBtnGameWonClicked()
        {
            GameSceneManager.Instance.LoadGameScene();
            
            GameManager.Instance.Init();
        
            gameObject.SetActive(false);
        }

        #endregion

        #region Event: OnBtnExitClicked

        private void OnBtnExitClicked()
        {
            GameSceneManager.Instance.LoadIntroScene();
            
            gameObject.SetActive(false);
        }

        #endregion

        #region AddEvents

        private void AddEvents()
        {
            btnGameWon.onClick.AddListener(OnBtnGameWonClicked);
            btnExit.onClick.AddListener(OnBtnExitClicked);
        }

        #endregion

        #region RemoveEvents

        private void RemoveEvents()
        {
            btnGameWon.onClick.RemoveAllListeners();
            btnExit.onClick.RemoveAllListeners();
        }

        #endregion
    }
}

