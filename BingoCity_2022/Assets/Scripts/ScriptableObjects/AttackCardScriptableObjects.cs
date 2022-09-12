using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackCard",menuName ="AttackCardStats")]
public class AttackCardScriptableObjects : ScriptableObject
{
    [Header("Attack")] [SerializeField]
    public List<AttackData> attackData;
    public List<AttackBuildingData> attackBuildingData;
    [SerializeField] private int raidToken;
    [SerializeField] private int raidCost;
    
    public int RaidToken
    {
        get => raidToken;
        set => raidToken = value;
    }
        
    public int RaidCost
    {
        get => raidCost;
        set => raidCost = value;
    }
    [Serializable]
    public class AttackData
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private int cardCount;
        [SerializeField] private int coinCount;
        [SerializeField] private int starCount;
       

        //public int UnlockedStar => unlockedStar;

        public Sprite Icon => icon;
        
        public int CoinCount
        {
            get => coinCount;
            set => coinCount = value;
        }
        
        public int CardCount
        {
            get => cardCount;
            set => cardCount = value;
        }
        public int StarCount => starCount;
    }

    [Serializable]
    public class AttackBuildingData
    {
        [SerializeField] private List<Sprite> buildingImages;
        [SerializeField] private int starCount;
        [SerializeField] private bool active;
        [SerializeField] private int starDestroyedCount;
        
        public int StarCount => starCount;
        
        public List<Sprite> BuildingImages => buildingImages;
        public bool Active
        {
            get => active;
            set => active = value;
        }
        
        public int StarDestroyedCount
        {
            get => starDestroyedCount;
            set => starDestroyedCount = value;
        }
    }
}