using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockBraker.Scripts.Scoreboards
{
    [Serializable]
    public class ScoreboardSaveData
    {
        public List<ScoreboardEntryData> highScores = new List<ScoreboardEntryData>();
    }
}


