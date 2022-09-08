using UnityEngine;
using TMPro;

public class AttackCardUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI AttackName;
    [SerializeField] private TextMeshProUGUI CardCount;
    [SerializeField] private TextMeshProUGUI CoinCount;

    public void AssigningValues(string name, int cardCount, int coinCount)
    {
        AttackName.text = name;
        CardCount.text = cardCount.ToString();
        CoinCount.text = coinCount.ToString();
    }
}
