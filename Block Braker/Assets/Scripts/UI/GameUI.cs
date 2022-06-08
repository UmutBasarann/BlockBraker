using BlockBraker.Scripts.Config.GameConfig;
using BlockBraker.Scripts.Enum;
using BlockBraker.Scripts.Gameplay;
using TMPro;
using UnityEngine;

namespace BlockBraker.Scripts.UI
{
    public class GameUI : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private TextMeshProUGUI txtScore = null;
        [SerializeField] private TextMeshProUGUI txtLives = null;

        #endregion

        #region Awake | Start | Update

        private void Start()
        {
            Block.OnBlockDestroyed += HandleBlockDestroyed;
            Ball.OnDeathZoneTriggered += HandleBallDeathZoneTriggered;
        }

        #endregion

        #region Event: HandleBallDeathZoneTriggered

        private void HandleBallDeathZoneTriggered(int lives)
        {
            txtLives.text = $"Lives: {lives}";
        }

        #endregion

        #region Event: HandleBlockDestroyed

        private void HandleBlockDestroyed(int score, BlockType blockType, Vector3 blockPosition, GameObject currentBlock)
        {
            GameConfig.CURRENT_GAME_SCORE += score;
            
            txtScore.text = $"Score: {GameConfig.CURRENT_GAME_SCORE}";
        }

        #endregion
    }
}

