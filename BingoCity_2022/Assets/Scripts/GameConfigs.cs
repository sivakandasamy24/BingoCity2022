using System;

public static class GameConfigs
{
    public static int NumberOfCardToPlay = 1;
    public static bool IsAutoDaubEnable = true;

    public static bool LoadDebugConfigPage;
    public static int cardSpanCount;
    public static int maxNumberRoll;
    public static int maxNumberOfBallPerClick;
    public static int buyAdditionalRollCount;
    //public static int LoadDebugConfig;

    public enum BingoLetters
    {
        BRow = 'B',
        IRow = 'I',
        NRow = 'N',
        GRow = 'G',
        ORow = 'O'
    }
}