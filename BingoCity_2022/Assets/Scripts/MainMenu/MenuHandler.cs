using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BingoCity
{
    public class MenuHandler : MonoBehaviour
    {
        private void Awake()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        public void LoadScene(int loadSceneIndex)
        {
            SceneManager.LoadScene(loadSceneIndex);
        }


    }
}