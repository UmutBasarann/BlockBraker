using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockBraker.Scripts.Managers.GameManager
{
    public class GameManager : MonoBehaviour
    {
        #region Event | Action

        public static event Action OnUpdate;

        #endregion

        #region Singleton

        private static GameManager _instance;
        public static GameManager Instance => _instance;

        #endregion

        #region Awake | Start | Update

        private void Awake()
        {
            if (_instance is null)
            {
                _instance = this;
            }

            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if (OnUpdate != null)
            {
                OnUpdate();
            }
        }

        #endregion
    }
}

