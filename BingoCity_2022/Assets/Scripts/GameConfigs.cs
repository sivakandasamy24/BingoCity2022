using System;

public static class GameConfigs
{
    public static int NumberOfCardToPlay = 1;

    public static bool IsAutoDaubEnable = true;

    public enum BingoLetters
    {
        BRow = 'B',
        IRow = 'I',
        NRow = 'N',
        GRow = 'G',
        ORow = 'O'
    }
}