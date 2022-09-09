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
        [SerializeField] private Button exitButton;

        public void OnEnable()
        {
            xpText.text = GameSummary.XpOnRound.ToString();
            coinText.text = GameSummary.coinsGained.ToString();
            raidTokenText.text = GameSummary.raidTokenGained.ToString();
        }


    }
}