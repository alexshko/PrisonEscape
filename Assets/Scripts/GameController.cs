﻿using UnityEngine;

namespace alexshko.prisonescape.Core
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance
        {
            get => instance;
        }
        private static GameController instance;

        private void Awake()
        {
            instance = this;
        }

        public void FinishGame()
        {
            Debug.Log("Finished Game.");
            Application.Quit();
        }
    }
}