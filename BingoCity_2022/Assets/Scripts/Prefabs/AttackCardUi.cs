using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AttackCardUi : MonoBehaviour
{
    [SerializeField] private Image AttackIcon;
    [SerializeField] private TextMeshProUGUI CardCount;
    [SerializeField] private TextMeshProUGUI CoinCount;

    public void AssigningValues(Sprite icon, int cardCount, int coinCount)
    {
        AttackIcon.sprite = icon;
        CardCount.text = cardCount.ToString();
        CoinCount.text = coinCount.ToString();
    }
}
