using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterCard", menuName = "CharacterCardStatistics")]
public class CharacterCardScriptableObjects : ScriptableObject
{
    [Header("Characters")] [SerializeField]
    public List<CharacterData> characterData;

    [Serializable]
    public class CharacterData
    {
        [SerializeField] private int unlockedStar;
        [SerializeField] private int tokenCollected;
        [SerializeField] private Sprite characterImage;
        [SerializeField] private List<int> starTokenCount;
        [SerializeField] private List<int> upgradeCoin;
        [SerializeField] private List<Sprite> upgradedImage;

        //public int UnlockedStar => unlockedStar;
        public int UnlockedStar
        {
            get => unlockedStar;
            set => unlockedStar = value;
        }
        public int TokenCollected => tokenCollected;
        public Sprite CharacterImage => characterImage;
        public List<int> StarTokenCount => starTokenCount;
        public List<int> UpgradeCoin => upgradeCoin;
        public List<Sprite> UpgradedImage => upgradedImage;
    }
}