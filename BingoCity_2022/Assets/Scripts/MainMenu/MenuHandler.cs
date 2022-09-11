using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BingoCity
{
    public class MenuHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI xpText;
        [SerializeField] private TextMeshProUGUI chipText;
        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private Text buyCardCostText;
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
            
            if (UserInventoryData.UserCoins < 0)
                UserInventoryData.UserCoins = 0;
            coinText.text = UserInventoryData.UserCoins.ToString();
            
            if (UserInventoryData.UserChips < 0)
                UserInventoryData.UserChips = 0;
            chipText.text = UserInventoryData.UserChips.ToString();
        }
        public void LoadScene(int loadSceneIndex)
        {
            var bingoCost = int.Parse(buyCardCostText.text);
            var userBalance = UserInventoryData.UserChips - bingoCost;
            
            if(userBalance<0)
                return;

            UserInventoryData.UserChips = userBalance;
            
            SceneManager.LoadScene(loadSceneIndex);
        }

        public void BuyCardNumbers(int cardNumbers)
        {
            GameConfigs.NumberOfCardToPlay = cardNumbers;
            buyCardCostText.text = (cardNumbers * 10).ToString();
        }


    }
}