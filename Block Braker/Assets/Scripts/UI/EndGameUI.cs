using BlockBraker.Scripts.Managers;
using BlockBraker.Scripts.Managers.GameManager;
using UnityEngine;
using UnityEngine.UI;

namespace BlockBraker.Scripts.UI
{
    public class EndGameUI : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private Button btnReturn = null;
        [SerializeField] private Button btnRestart = null;

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
        
        #region Event: OnBtnReturnClicked

        private void OnBtnReturnClicked()
        {
            GameSceneManager.Instance.LoadIntroScene();
            GameSceneManager.Instance.Start();
            
            gameObject.SetActive(false);
        }

        #endregion

        #region Event: OnBtnRestartClicked

        private void OnBtnRestartClicked()
        {
            GameSceneManager.Instance.LoadGameScene();
            GameManager.Instance.Init();
            
            gameObject.SetActive(false);
        }

        #endregion

        #region AddEvents

        private void AddEvents()
        {
            btnRestart.onClick.AddListener(OnBtnRestartClicked);
            btnReturn.onClick.AddListener(OnBtnReturnClicked);
        }

        #endregion

        #region RemoveEvents

        private void RemoveEvents()
        {
            btnRestart.onClick.RemoveAllListeners();
            btnReturn.onClick.RemoveAllListeners();
        }

        #endregion
    }
}

