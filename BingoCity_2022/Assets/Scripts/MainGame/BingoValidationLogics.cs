using System.Collections.Generic;
using UnityEngine;

namespace BingoCity
{
    public class BingoValidationLogics
    {
       
        public static Dictionary<string, List<int>> CheckHorizontalPattern(List<BingoCell> cellObjArr)
        {
            var isBingoFound = false;
            var bingoSeqCount = 0;
            Dictionary<string, List<int>> winBingoDetails = new();
            for (var rowCount = 0; rowCount < 5; rowCount++)
            {
                var winBingoCells = new List<int>();
                var nextCellId = rowCount;
                for (var colCount = 0; colCount < 5; colCount++)
                {
                    var cell = cellObjArr[nextCellId];

                    if (cell == null || cell.IsDaubed)
                    {
                        winBingoCells.Add(cell.CellId);
                        nextCellId += 5;
                        bingoSeqCount++;
                    }
                    else
                    {
                        bingoSeqCount = 0;
                        break;
                    }
                }

                if (bingoSeqCount > 4)
                {
                    isBingoFound = true;
                    winBingoDetails.Add($"H_{rowCount}",winBingoCells);
                   // break;
                }
            }

            Debug.Log("--CheckHorizontalPattern--" + isBingoFound);
            return winBingoDetails;
        }

        public static Dictionary<string, List<int>> CheckVerticalPattern(List<BingoCell> cellObjArr)
        {
            var isBingoFound = false;
            var bingoSeqCount = 0;
            Dictionary<string, List<int>> winBingoDetails = new();
            for (var colCount = 0; colCount < 5; colCount++)
            {
                var winBingoCells = new List<int>();
                var nextCellId = colCount*5;
                for (var rowCount = 0; rowCount < 5; rowCount++)
                {
                    var cell = cellObjArr[nextCellId];

                    if (cell == null || cell.IsDaubed)
                    {
                        winBingoCells.Add(cell.CellId);
                        nextCellId++;
                        bingoSeqCount++;
                    }
                    else
                    {
                        bingoSeqCount = 0;
                        break;
                    }
                }
                
                if (bingoSeqCount > 4)
                {
                    isBingoFound = true;
                    winBingoDetails.Add($"V_{colCount}",winBingoCells);
                    // break;
                }
            }

            Debug.Log("--CheckVerticalPattern--" + isBingoFound);
            return winBingoDetails;
        }

        public static Dictionary<string, List<int>> CheckDiagonalPattern(List<BingoCell> cellObjArr)
        {
            var isBingoFound = false;
            Dictionary<string, List<int>> winBingoDetails = new();
            if (
                IsCellDaubed(cellObjArr[0]) &&
                IsCellDaubed(cellObjArr[6]) &&
                IsCellDaubed(cellObjArr[12]) &&
                IsCellDaubed(cellObjArr[18]) &&
                IsCellDaubed(cellObjArr[24])
            )
            {
                winBingoDetails.Add("D1",new List<int>(){0,6,12,18,24});
                isBingoFound = true;
            }
            if (
                IsCellDaubed(cellObjArr[4]) &&
                IsCellDaubed(cellObjArr[8]) &&
                IsCellDaubed(cellObjArr[12]) &&
                IsCellDaubed(cellObjArr[16]) &&
                IsCellDaubed(cellObjArr[20])
            )
            {
                winBingoDetails.Add("D2",new List<int>(){4,8,12,16,20});
                isBingoFound = true;
            }

            Debug.Log("--CheckDiagonalPattern--" + isBingoFound);

            return winBingoDetails;
        }

        private static bool IsCellDaubed(BingoCell cell)
        {
            return cell == null || cell.IsDaubed;
        }
    }
}