using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BingoCity.Popups
{
    public class SummaryPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI xpText;
        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private TextMeshProUGUI raidTokenText;
        [SerializeField] private GameObject tokenIconContainer;
        [SerializeField] private TokenIconSummary tokenIcon;
        [SerializeField] private Text totalBingosText;
        [SerializeField] private Button exitButton;

        public void OnEnable()
        {
            RemoveAllChild(tokenIconContainer);
            foreach (var tokenGained in GameSummary.cityBuildTokenGained)
            {
                var inventoryData = GameConfigs.InventoryAssetData.GetInventoryData(tokenGained.Key);
                var tokenIconObj = Instantiate(tokenIcon, tokenIconContainer.transform);
                tokenIconObj.SetData(inventoryData.GsSummaryIcon,tokenGained.Value);
            }
            xpText.text = GameSummary.XpOnRound.ToString();
            coinText.text = GameSummary.coinsGained.ToString();
            raidTokenText.text = GameSummary.raidTokenGained.ToString();
            totalBingosText.text = $"Total Bingos : {GameSummary.bingoGained}";
        }
        private  void RemoveAllChild(GameObject parent)
        {
            foreach (Transform child in parent.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
    
}