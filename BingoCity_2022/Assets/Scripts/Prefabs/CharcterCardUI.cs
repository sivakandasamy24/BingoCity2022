using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharcterCardUI : MonoBehaviour
{
    [SerializeField] private GameObject[] RatingGameObjects;
    [SerializeField] private Image Character;
    [SerializeField] private TextMeshProUGUI CoinCount;
    [SerializeField] private TextMeshProUGUI Tokens;

    private static int _firstLevel = 5;
    private static int _secondLevel = 10;
    private static int _thirdLevel = 15;


    public bool AssigningCharacterCardValues(int ratings,int tokensCollected, int coinCount, Sprite character = null)
    {
        for (int i = 0; i < ratings; i++)
        {
            RatingGameObjects[i].SetActive(true);
        }
        Tokens.text = tokensCollected + "/" + _firstLevel;
        Character.sprite = character;
        CoinCount.text = coinCount.ToString();
        return tokensCollected >= _firstLevel;
    }
}
