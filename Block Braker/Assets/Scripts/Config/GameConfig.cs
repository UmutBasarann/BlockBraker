using System;
using Random = UnityEngine.Random;

namespace BlockBraker.Scripts.Config.GameConfig
{
    public static class GameConfig
    {

        #region Scene

        public const string GAME_SCENE_ID = "Game";
        public const string INTRO_SCENE_ID = "Intro";
        public const string HIGHSCORE_SCENE_ID = "HighScore";

        #endregion

        #region PlayerPrefs

        public const string HIGH_SCORE_TABLE_PREF_NAME = "HighScoreTable";

        #endregion

        public static int CURRENT_GAME_SCORE = 0;
        public static int CURRENT_GAME_LEVEL = 0;
    }
}