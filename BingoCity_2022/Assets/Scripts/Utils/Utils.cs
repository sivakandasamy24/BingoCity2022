using System.Collections.Generic;
using UnityEngine;

namespace BingoCity
{
    public class Utils
    {
        private static readonly List<int> RandomList = new ();
        
        
        private static readonly List<int> BallCallingList = new();
        public static  List<int> BCardNumbers = new();
        public static  List<int> ICardNumbers = new();
        public static  List<int> NCardNumbers = new();
        public static  List<int> GCardNumbers = new();
        public static  List<int> OCardNumbers = new();

        public static int ballCallingSpan;

        private static void GenerateCardsBasedOnSpan()
        {
            BCardNumbers = GetNumbers(1, 15);
            ICardNumbers = GetNumbers(16, 30);
            NCardNumbers = GetNumbers(31, 45);
            GCardNumbers = GetNumbers(46, 60);
            OCardNumbers = GetNumbers(61, 75);


            GetBallCallingSpanList();
        }

        private static void GetBallCallingSpanList()
        {
            BallCallingList.AddRange( BCardNumbers);
            BallCallingList.AddRange( ICardNumbers);
            BallCallingList.AddRange( NCardNumbers);
            BallCallingList.AddRange( GCardNumbers);
            BallCallingList.AddRange( OCardNumbers);
        }

        private static List<int> GetNumbers(int minNumber, int maxNumber)
        {
            var generatedNumbers = new List<int>();
            for (int i = minNumber; i < maxNumber + 1; i++)
            {
                generatedNumbers.Add(i);
            }

            generatedNumbers.Shuffle();

            var spawnValue = ballCallingSpan / 5;
            return generatedNumbers.GetRange(0, spawnValue);
        }
        

        public static int GetRandUnCalledBallNumber()
        {
            if (BallCallingList.Count < 1) GetBallCallingSpanList();

            var randNumber = BallCallingList.GetAndRemoveRandomValue();
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
            BCardNumbers.Clear();
            ICardNumbers.Clear();
            NCardNumbers.Clear();
            GCardNumbers.Clear();
            OCardNumbers.Clear();
            BallCallingList.Clear();
            RandomList.Clear();
            BallCallingList.Clear();
            
            GenerateCardsBasedOnSpan();
        }
    }
}