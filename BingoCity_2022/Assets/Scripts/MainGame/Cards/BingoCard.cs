using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;

namespace BingoCity
{
    public class BingoCard : MonoBehaviour
    {
        [SerializeField] private List<BingoCell> bingoCells;
        [SerializeField] private GameObject bingoAnimation;
        [SerializeField] private TextMeshProUGUI cardIdText;

        private int _cardId;
        private int _isBingoCalled;
        private readonly Dictionary<int, BingoCell> _bingoCells = new();
        private List<int> _itemPattern;
        private CardItemManager _itemManager;
        private readonly Dictionary<int, int> _itemPatternData = new();
        private int _daubsRemainingCount;

        private List<int> _bRowCards;
        private List<int> _iRowCards;
        private List<int> _nRowCards;
        private List<int> _gRowCards;
        private List<int> _oRowCards;
        private readonly Dictionary<string, List<int>> _winBingoData = new();

        

        public void SetData(int cardId, List<int> itemPattern )
        {
            _cardId = cardId;
            _itemPattern = itemPattern;
            _itemManager = GetComponent<CardItemManager>();
            cardIdText.text = $"Card {cardId+1}";
            GetItemPatternData();
            GenerateCardNumbers();
        }

        private void GetItemPatternData()
        {
            foreach (var patternId in _itemPattern)
            {
                if (patternId < 1) continue;

                if (_itemPatternData.ContainsKey(patternId))
                {
                    _itemPatternData[patternId]++;
                }
                else
                {
                    _itemPatternData.Add(patternId, 1);
                }
            }
        }

        private int GetUniqueCardRandomNumber(int cellId)
        {
            var randNumber = 0;
            var selectedRowList = _bRowCards;

            switch (cellId)
            {
                case >= 0 and <= 4:
                    selectedRowList = _bRowCards;
                    break;
                case >= 5 and <= 9:
                    selectedRowList = _iRowCards;
                    break;
                case >= 10 and <= 14:
                    selectedRowList = _nRowCards;
                    break;
                case >= 15 and <= 19:
                    selectedRowList = _gRowCards;
                    break;
                case >= 20:
                    selectedRowList = _oRowCards;
                    break;
            }

            randNumber = selectedRowList.GetAndRemoveRandomValue();

            return randNumber;
        }

        private void GetCardRowsClone()
        {
            _bRowCards = Utils.BCardNumbers.GetClone();
            _iRowCards = Utils.ICardNumbers.GetClone();
            _nRowCards = Utils.NCardNumbers.GetClone();
            _gRowCards = Utils.GCardNumbers.GetClone();
            _oRowCards = Utils.OCardNumbers.GetClone();
        }

        private void GenerateCardNumbers()
        {
            GetCardRowsClone();
            ResetCard();
            for (int i = 0; i < bingoCells.Count; i++)
            {
                var bingoCell = bingoCells[i];
                var randNumber = GetUniqueCardRandomNumber(i);
                if (bingoCell != null)
                {
                    if (_itemPattern[i] > 0)
                    {
                        _itemManager.SetItemPosition(_itemPattern[i],
                            bingoCell.GetComponent<RectTransform>().anchoredPosition);
                    }

                    bingoCell.SetData(randNumber, i, OnUserDaub);
                    _bingoCells.Add(randNumber, bingoCell);
                }
            }
        }

        private void OnUserDaub(int cellNumber)
        {
            DoAutoDaub(new List<int> { cellNumber });
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

                _daubsRemainingCount--;
                bingoCell.DoMarkCellAsDaub();
                _bingoCells.Remove(ballNumber);
            }

            var winBingoDetails = BingoValidationLogics.CheckHorizontalPattern(bingoCells);
            foreach (var winRowColDetails in winBingoDetails)
            {
                if (!_winBingoData.ContainsKey(winRowColDetails.Key))
                {
                    _winBingoData.Add(winRowColDetails.Key, winRowColDetails.Value);
                    ShowBingoIconOnCell(winRowColDetails.Value);
                }
            }

            CheckAndShowBingoPatterns(BingoValidationLogics.CheckHorizontalPattern(bingoCells));
            CheckAndShowBingoPatterns(BingoValidationLogics.CheckVerticalPattern(bingoCells));
            CheckAndShowBingoPatterns(BingoValidationLogics.CheckDiagonalPattern(bingoCells));

            /*if (BingoValidationLogics.CheckHorizontalPattern(bingoCells) ||
                BingoValidationLogics.CheckVerticalPattern(bingoCells) ||
                BingoValidationLogics.CheckDiagonalPattern(bingoCells))
            {
                print("******************** BINGO ********************");
            }*/
        }

        private void CheckAndShowBingoPatterns(Dictionary<string, List<int>> winBingoDetails)
        {
            foreach (var winRowColDetails in winBingoDetails)
            {
                if (!_winBingoData.ContainsKey(winRowColDetails.Key))
                {
                    _winBingoData.Add(winRowColDetails.Key, winRowColDetails.Value);
                    ShowBingoIconOnCell(winRowColDetails.Value);
                }
            }
        }

        private void ShowBingoIconOnCell(List<int> winBingoCells)
        {
            UpdateSummaryRewards();
            PlayBingoAnimation();
            foreach (var cellsId in winBingoCells)
            {
                bingoCells[cellsId].ShowWinBingoIcon();
            }
        }

        private void UpdateSummaryRewards()
        {
            var totCoinRwdCount = GameConfigs.GameConfigData.CoinReward.Count - 1;
            if (_winBingoData.Count > totCoinRwdCount)
            {
                GameSummary.coinsGained +=
                    GameConfigs.GameConfigData.CoinReward[totCoinRwdCount];
            }
            else
            {
                GameSummary.coinsGained += GameConfigs.GameConfigData.CoinReward[_winBingoData.Count-1];
            }

            GameSummary.bingoGained++;
            EventManager.onBingoEvent?.Invoke();
        }

        private void PlayBingoAnimation()
        {
            if(!gameObject.activeInHierarchy) return;
            
            var animator = bingoAnimation.GetComponent<Animator>();
            

            var offsetDelay = 0.75f;
            var bingoAnimationDuration = Utils.GetAnimationDuration(animator, "Tourney_anim_BingoAnim_1");
            GameConfigs.BingoAnimPlayTime = bingoAnimationDuration+offsetDelay;
            
            print($"--time--PlayBingoAnimation start--- {GameConfigs.BingoAnimPlayTime}");
            
            Observable.ReturnUnit()
                .Delay(TimeSpan.FromSeconds(offsetDelay))
                .Do(_ =>
                {
                    bingoAnimation.SetActive(true);
                    SoundUtils.PlaySoundOnce(AudioTrackNames.GsBingo);
                    animator.Play("Bingo", -1, 0f);
                })
                .Delay(TimeSpan.FromSeconds(bingoAnimationDuration))
                .Do(_ =>
                {
                    print($"--time--PlayBingoAnimation End--- {GameConfigs.BingoAnimPlayTime}");

                    GameConfigs.BingoAnimPlayTime = 0;
                })
                .Subscribe().AddTo(this);
        }

        private void ResetCard()
        {
            _daubsRemainingCount = bingoCells.Count;
            GameConfigs.BingoAnimPlayTime = 0;
            bingoAnimation.SetActive(false);
            _winBingoData.Clear();
            _bingoCells.Clear();
            _itemManager.ResetItems();
        }

        public bool IsAllCellDaubed()
        {
            return _daubsRemainingCount <= 0;
        }
    }
}