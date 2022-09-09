using System;
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
    //public static int LoadDebugConfig;

    public static float BingoAnimPlayTime;
    
    public static GameConfigData GameConfigData;

    public enum BingoLetters
    {
        BRow = 'B',
        IRow = 'I',
        NRow = 'N',
        GRow = 'G',
        ORow = 'O'
    }
}

public static class GameSummary
{
    public static int XpOnRound;
    public static int coinsGained;
    public static int bingoGained;
    public static int raidTokenGained;
    public static int cityBuildTokenGained;

    public static void ResetData()
    {
        XpOnRound = 0;
        coinsGained = 0;
        bingoGained = 0;
        raidTokenGained = 0;
        cityBuildTokenGained = 0;
    }
}