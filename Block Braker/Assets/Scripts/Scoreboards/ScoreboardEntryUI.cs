using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BlockBraker.Scripts.Scoreboards
{
    public class ScoreboardEntryUI : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private TextMeshProUGUI txtScore = null;

        #endregion

        #region Init

        public void Init(ScoreboardEntryData scoreboardEntryData)
        {
            txtScore.text = scoreboardEntryData.entryScore.ToString();
        }

        #endregion
    }
}

