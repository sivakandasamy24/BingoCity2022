using System;
using System.Collections.Generic;
using BingoCity;

public static class GameConfigs
{
    public static int NumberOfCardToPlay = 2 ;
    public static bool IsAutoDaubEnable = true;

    public static bool LoadDebugConfigPage;
    public static int cardSpanCount;
    public static int maxNumberRoll;
    public static int maxNumberOfBallPerClick;
    public static int buyAdditionalRollCount;
    public static int timerDuration;
    public static float BingoAnimPlayTime;
    
    public static GameConfigData GameConfigData;
    public static InventoryData InventoryAssetData;

    public enum BingoLetters
    {
        BRow = 'B',
        IRow = 'I',
        NRow = 'N',
        GRow = 'G',
        ORow = 'O'
    }
}

public static class UserInventoryData
{
    public static int UserCurrentLevel = 1;
    public static int UserXpcount;
    public static int UserCoins;
    private static readonly Dictionary<int, int> UserTokenData = new Dictionary<int, int>();
    public static void UpdateUserInventory(int inventoryId,int countGained)
    {
        if (UserTokenData.ContainsKey(inventoryId))
        {
            UserTokenData[inventoryId]+=countGained;
        }
        else
        {
            UserTokenData.Add(inventoryId,countGained);
        }
        
    }
}
public static class GameSummary
{
    private static int XpOnRound1;
    private static int coinsGained1;

    public static int XpOnRound
    {
        get => XpOnRound1;
        set
        {
            XpOnRound1 = value;
            UserInventoryData.UserXpcount += value;
        }
    }

    public static int coinsGained
    {
        get => coinsGained1;
        set
        {
            coinsGained1 = value;
            UserInventoryData.UserCoins += value;
        }
    }

    
    public static int bingoGained;
    public static int raidTokenGained;
    public static readonly Dictionary<int, int> cityBuildTokenGained = new();

    public static void ResetData()
    {
        XpOnRound = 0;
        coinsGained = 0;
        bingoGained = 0;
        raidTokenGained = 0;
        cityBuildTokenGained .Clear();
    }
    
    public static void UpdateInventoryRewards(int inventoryId,int countGained)
    {
        if (cityBuildTokenGained.ContainsKey(inventoryId))
        {
            cityBuildTokenGained[inventoryId]+=countGained;
        }
        else
        {
            cityBuildTokenGained.Add(inventoryId,countGained);
        }

        UserInventoryData.UpdateUserInventory(inventoryId,countGained);
    }
}