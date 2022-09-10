using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BingoCity
{
    public class MenuHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI xpText;
        [SerializeField] private TextMeshProUGUI chipText;
        [SerializeField] private TextMeshProUGUI coinText;
        private void Awake()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private void OnEnable()
        {
            UpdateTopHudData();
        }

        private void UpdateTopHudData()
        {
            xpText.text = UserInventoryData.UserXpcount.ToString();
            coinText.text = UserInventoryData.UserCoins.ToString();
            chipText.text = UserInventoryData.UserChips.ToString();
        }
        public void LoadScene(int loadSceneIndex)
        {
            SceneManager.LoadScene(loadSceneIndex);
        }

        public void BuyCardNumbers(int cardNumbers)
        {
            GameConfigs.NumberOfCardToPlay = cardNumbers;
        }


    }
}