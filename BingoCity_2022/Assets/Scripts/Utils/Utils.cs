using System.Collections.Generic;
using UnityEngine;

namespace BingoCity
{
    public class Utils
    {
        private static readonly List<int> RandomList = new List<int>();
        private static readonly List<int> BallCallingArr = new List<int>();

        public static int GetUniqueCardRandomNumber(int cellId)
        {
            var randNumber = 0;
            var minNumber = 1;
            var maxNumber = 15;

            switch (cellId)
            {
                case >= 0 and <= 4:
                    minNumber = 1;
                    maxNumber = 15;
                    break;
                case >= 5 and <= 9:
                    minNumber = 16;
                    maxNumber = 30;
                    break;
                case >= 10 and <= 14:
                    minNumber = 31;
                    maxNumber = 45;
                    break;
                case >= 15 and <= 19:
                    minNumber = 46;
                    maxNumber = 60;
                    break;
                case >= 20:
                    minNumber = 61;
                    maxNumber = 75;
                    break;
            }

            randNumber = Random.Range(minNumber, maxNumber);
            while (RandomList.Contains(randNumber))
            {
                randNumber = Random.Range(minNumber, maxNumber);
            }

            RandomList.Add(randNumber);
            return randNumber;
        }

        public static int GetRandUnCalledBallNumber()
        {
            if (BallCallingArr.Count < 1) ResetBallNumbers();

            var randNumber = BallCallingArr.GetAndRemoveRandomValue();
            return randNumber;
        }

        public static GameConfigs.BingoLetters GetLetterByValue(int number)
        {
            var rowString = GameConfigs.BingoLetters.ORow;

            if (number >= 0 && number <= 15)
                rowString = GameConfigs.BingoLetters.BRow;
            else if (number >= 16 && number <= 30)
                rowString = GameConfigs.BingoLetters.IRow;
            else if (number >= 31 && number <= 45)
                rowString = GameConfigs.BingoLetters.NRow;
            else if (number >= 46 && number <= 60)
                rowString = GameConfigs.BingoLetters.GRow;
           

            return rowString;
        }

        public static void ResetGameData()
        {
            RandomList.Clear();
            ResetBallNumbers();
        }

        private static void ResetBallNumbers()
        {
            BallCallingArr.Clear();
            for (var i = 1; i < 76; i++)
            {
                BallCallingArr.Add(i);
            }

            BallCallingArr.Shuffle();
        }
    }
}