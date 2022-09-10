using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharcterCardUI : MonoBehaviour
{
    [SerializeField] private Star[] RatingGameObjects;
    [SerializeField] private Button cardButton;
    [SerializeField] private Image Character;
    [SerializeField] private TextMeshProUGUI CoinCount;
    [SerializeField] private TextMeshProUGUI Tokens;
    //[SerializeField] private CharacterManager _characterManager;

    private static int _firstLevel = 5;
    private static int _secondLevel = 10;
    private static int _thirdLevel = 15;
    
    private static int _firstLevelUpgradeCoinCost = 500;
    private static int _secondLevelUpgradeCoinCost = 1000;
    private static int _thirdLevelUpgradeCoinCost = 1500;

    private int _userCoins = 5000;
    private int _upgradeCoinAmout = 0;
    private int _unlockedStar = 0;
    private CharacterCardScriptableObjects.CharacterData _characterData;
    public CharacterManager _characterManager;
    public Button CardButton => cardButton;

    private void Awake()
    {
        cardButton.enabled = false;
    }
    
    public void AssigningCharacterCardValues(CharacterCardScriptableObjects.CharacterData characterData, CharacterManager characterManager)
    {
        _characterManager = characterManager;
        _characterData = characterData;
        _unlockedStar = GetUnlockedStar(characterData.TokenCollected, characterData.StarTokenCount);
        if (_unlockedStar == 0)
        {
            Tokens.text = characterData.TokenCollected + "/" + characterData.StarTokenCount[0];
            CoinCount.text = characterData.UpgradeCoin[0].ToString();
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
            
                if (_unlockedStar == i+1 && characterData.TokenCollected >= characterData.StarTokenCount[i])
                {
                    if (_characterData.UnlockedStar < _unlockedStar)
                    {
                        cardButton.enabled = true;
                    }
                    Tokens.text = characterData.TokenCollected + "/" + characterData.StarTokenCount[i];
                    CoinCount.text = characterData.UpgradeCoin[i].ToString();
                    _upgradeCoinAmout = characterData.UpgradeCoin[i];
                }
                else
                {
                    if (_unlockedStar > 0)
                    {
                        if (characterData.TokenCollected > characterData.StarTokenCount[i - 1])
                        {
                            Tokens.text = (characterData.TokenCollected -  characterData.StarTokenCount[i - 1])+ "/" + characterData.StarTokenCount[i];
                            CoinCount.text = characterData.UpgradeCoin[i].ToString();
                            RatingGameObjects[i - 1].SetStarActive();
                            _characterManager.AttachUpgradedCharacter(i , characterData.UpgradedImage[i - 1]);
                        }
                            
                    }
                    else
                    {
                        Tokens.text = characterData.TokenCollected + "/" + characterData.StarTokenCount[i];
                        CoinCount.text = characterData.UpgradeCoin[i].ToString();
                        if (characterData.TokenCollected <= 0)
                            break;
                    }
                
                }
            }
        }
        Character.sprite = characterData.CharacterImage;
    }
    private int GetUnlockedStar(int currentTokenCollected, List<int> starTokenCount)
    {
        var currentStarUnlocked = 0;
        foreach (var tokenCount in starTokenCount)
        {
            if (tokenCount <= currentTokenCollected)
            {
                ++currentStarUnlocked;
            }
        }

        return currentStarUnlocked;
    }

    public void UpgradeCharacter()
    {
        if (_upgradeCoinAmout <= _userCoins)
        {
            _characterData.UnlockedStar = _unlockedStar;
            _userCoins = _userCoins - _upgradeCoinAmout;
            RatingGameObjects[_unlockedStar - 1].SetStarActive();
            Tokens.text = "0" + "/" + _characterData.StarTokenCount[_unlockedStar];
            _characterManager.AttachUpgradedCharacter(_unlockedStar - 1, _characterData.UpgradedImage[_unlockedStar - 1]);
        }
        else
        {
            Debug.Log("Not enough coins");
        }
    }
}
