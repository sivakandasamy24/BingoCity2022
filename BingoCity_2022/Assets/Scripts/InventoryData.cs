using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BingoCity
{
    [CreateAssetMenu(menuName = "Scriptable/InventoryData", fileName = "InventoryData", order = 0)]
    public class InventoryData : ScriptableObject
    {
        [SerializeField] private List<InventoryMultiLevelData> inventoryMultiLevelData;

        public InventoryLevelData GetInventoryData(int inventoryId)
        {
            var selectInventory = inventoryMultiLevelData.Find(x => x.InventoryId == inventoryId);
            return  selectInventory?.LevelData.Find(x => x.Level == UserInventoryData.UserCurrentLevel);
        }
    }

    [Serializable]
    public class InventoryMultiLevelData
    {
        [SerializeField] private int inventoryId;
        [SerializeField] private List<InventoryLevelData> levelData;
        
        public int InventoryId => inventoryId;

        public List<InventoryLevelData> LevelData => levelData;
    }
    [Serializable]
    public class InventoryLevelData
    {
        [SerializeField] private int level;
        [SerializeField] private Sprite gsCardImage;
        [SerializeField] private Sprite gsSummaryIcon;

        public int Level => level;
        public Sprite GsCardImage => gsCardImage;
        public Sprite GsSummaryIcon => gsSummaryIcon;
    }
}