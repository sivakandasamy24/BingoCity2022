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
        private readonly Dictionary<int, BingoCell> _bingoCells = new ();
        private List<int> _itemPattern;
        private CardItemManager _itemManager;
        private readonly Dictionary<int, int> _itemPatternData = new ();

        public void SetData(int cardId,List<int> itemPattern)
        {
            _cardId = cardId;
            _itemPattern = itemPattern;
            _itemManager = GetComponent<CardItemManager>();
            GetItemPatternData();
            GenerateCardNumbers();
        }

        private void GetItemPatternData()
        {
            foreach (var patternId in _itemPattern)
            {
                if(patternId<1) continue;
                
                if (_itemPatternData.ContainsKey(patternId))
                {
                    _itemPatternData[patternId]++;
                }
                else
                {
                    _itemPatternData.Add(patternId,1);
                }
            }
            
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

                var cellId = bingoCell.CellId;
                var daubedPatternId = _itemPattern[cellId];
                if (daubedPatternId > 0)
                {
                    var pattenRevealCount = _itemPatternData[daubedPatternId];
                    if (pattenRevealCount > 0)
                    {
                        _itemPatternData[daubedPatternId]--;
                        if (_itemPatternData[daubedPatternId] <= 0)
                        {
                            _itemManager.RevealItem(daubedPatternId);
                        }
                    }
                    
                }
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