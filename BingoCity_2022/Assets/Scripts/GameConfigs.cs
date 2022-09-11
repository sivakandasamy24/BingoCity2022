using System;
using System.Collections.Generic;
using BingoCity;

public static class GameConfigs
{
    public static int NumberOfCardToPlay = 2;
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
    public static int UserCoins = 1000;
    public static int UserChips = 1000;
    public static readonly Dictionary<int, int> UserTokenData = new Dictionary<int, int>();

    public static void UpdateUserInventory(int inventoryId, int countGained)
    {
        if (UserTokenData.ContainsKey(inventoryId))
        {
            UserTokenData[inventoryId] += countGained;
        }
        else
        {
            UserTokenData.Add(inventoryId, countGained);
        }
    }
}

public static class GameSummary
{
    public static int XpOnRound;
    public static int coinsGained;
    public static int bingoGained;
    public static int raidTokenGained;
    public static readonly Dictionary<int, int> cityBuildTokenGained = new();

    public static void UpdateRewardsToUserAccount()
    {
        UserInventoryData.UserCoins += coinsGained;
        UserInventoryData.UserXpcount += XpOnRound;
        foreach (var tokenGained in cityBuildTokenGained)
        {
            UserInventoryData.UpdateUserInventory(tokenGained.Key, tokenGained.Value);
        }
    }
    public static void ResetData()
    {
        XpOnRound = 0;
        coinsGained = 0;
        bingoGained = 0;
        raidTokenGained = 0;
        cityBuildTokenGained.Clear();
    }

    public static void UpdateInventoryRewards(int inventoryId, int countGained)
    {
        if (cityBuildTokenGained.ContainsKey(inventoryId))
        {
            cityBuildTokenGained[inventoryId] += countGained;
        }
        else
        {
            cityBuildTokenGained.Add(inventoryId, countGained);
        }
    }
}