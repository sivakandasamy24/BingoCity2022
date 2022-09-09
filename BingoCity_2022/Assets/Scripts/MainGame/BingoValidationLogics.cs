using System.Collections.Generic;
using UnityEngine;

namespace BingoCity
{
    public class BingoValidationLogics
    {
        private static readonly Dictionary<string, List<int>> _winBingoDetails = new();
        public static bool CheckHorizontalPattern(List<BingoCell> cellObjArr)
        {
            var isBingoFound = false;
            var nextCellId = 0;
            var bingoSeqCount = 0;
            _winBingoDetails.Clear();
            var _winBingoCells = new List<int>();
            for (var rowCount = 0; rowCount < 5; rowCount++)
            {
                for (var colCount = 0; colCount < 5; colCount++)
                {
                    var cell = cellObjArr[nextCellId];

                    if (cell == null || cell.IsDaubed)
                    {
                        nextCellId += 5;
                        bingoSeqCount++;
                    }
                    else
                    {
                        nextCellId = rowCount + 1;
                        bingoSeqCount = 0;
                        break;
                    }
                }

                if (bingoSeqCount > 4)
                {
                    isBingoFound = true;
                    break;
                }
            }

            Debug.Log("--CheckHorizontalPattern--" + isBingoFound);
            return isBingoFound;
        }

        public static bool CheckVerticalPattern(List<BingoCell> cellObjArr)
        {
            var isBingoFound = false;
            var nextCellId = 0;
            var bingoSeqCount = 0;

            for (var colCount = 0; colCount < 5; colCount++)
            {
                for (var rowCount = 0; rowCount < 5; rowCount++)
                {
                    var cell = cellObjArr[nextCellId];

                    if (cell == null || cell.IsDaubed)
                    {
                        nextCellId++;
                        bingoSeqCount++;
                    }
                    else
                    {
                        nextCellId = (colCount + 1) * 5;
                        bingoSeqCount = 0;
                        break;
                    }
                }

                if (bingoSeqCount > 4)
                {
                    isBingoFound = true;
                    break;
                }
            }

            Debug.Log("--CheckVerticalPattern--" + isBingoFound);
            return isBingoFound;
        }

        public static bool CheckDiagonalPattern(List<BingoCell> cellObjArr)
        {
            var isBingoFound = false;

            if (
                IsCellDaubed(cellObjArr[0]) &&
                IsCellDaubed(cellObjArr[6]) &&
                IsCellDaubed(cellObjArr[12]) &&
                IsCellDaubed(cellObjArr[18]) &&
                IsCellDaubed(cellObjArr[24])
            )
            {
                isBingoFound = true;
            }
            else if (
                IsCellDaubed(cellObjArr[4]) &&
                IsCellDaubed(cellObjArr[8]) &&
                IsCellDaubed(cellObjArr[12]) &&
                IsCellDaubed(cellObjArr[16]) &&
                IsCellDaubed(cellObjArr[20])
            )
            {
                isBingoFound = true;
            }

            Debug.Log("--CheckDiagonalPattern--" + isBingoFound);

            return isBingoFound;
        }

        private static bool IsCellDaubed(BingoCell cell)
        {
            return cell == null || cell.IsDaubed;
        }
    }
}