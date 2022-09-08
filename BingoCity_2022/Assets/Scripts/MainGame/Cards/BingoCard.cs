using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BingoCity
{
    public class BingoCard : MonoBehaviour
    {
        [SerializeField] private List<BingoCell> bingoCells;
        

        private int _cardId;
        private int _isBingoCalled;
        private readonly Dictionary<int, BingoCell> _bingoCells = new Dictionary<int, BingoCell>();
        private List<int> _itemPattern;
        private CardItemManager _itemManager;

        public void SetData(int cardId,List<int> itemPattern)
        {
            _cardId = cardId;
            _itemPattern = itemPattern;
            _itemManager = GetComponent<CardItemManager>();
            GenerateCardNumbers();
        }

        private void GenerateCardNumbers()
        {
            ResetCard();
            for (int i = 0; i < bingoCells.Count; i++)
            {
                var bingoCell = bingoCells[i];
                var randNumber = Utils.GetUniqueCardRandomNumber(i);
                if (bingoCell != null)
                {
                    if (_itemPattern[i] > 0)
                    {
                        _itemManager.SetItemPosition(_itemPattern[i],bingoCell.GetComponent<RectTransform>().anchoredPosition);
                    }
                    bingoCell.SetData(randNumber,i,OnUserDaub);
                    _bingoCells.Add(randNumber,bingoCell);
                }
                
            }
        }

        private void OnUserDaub(int cellNumber)
        {
            DoAutoDaub(new List<int>{cellNumber});
        }
        public void DoAutoDaub(List<int> calledBalls)
        {
            foreach (var ballNumber in calledBalls)
            {
                if (!_bingoCells.ContainsKey(ballNumber)) continue;

                var bingoCell = _bingoCells[ballNumber];
                if (bingoCell == null || bingoCell.IsDaubed) continue;

                bingoCell.DoMarkCellAsDaub();
                _bingoCells.Remove(ballNumber);
            }

            if (BingoValidationLogics.CheckHorizontalPattern(bingoCells) ||
                BingoValidationLogics.CheckVerticalPattern(bingoCells) ||
                BingoValidationLogics.CheckDiagonalPattern(bingoCells))
            {
                print("******************** BINGO ********************");
            }
        }

        private void ResetCard()
        {
            _bingoCells.Clear();
            _itemManager.ResetItems();
        }
    }
}