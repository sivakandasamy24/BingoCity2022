using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace BingoCity
{
    public class BingoCardManager : MonoBehaviour
    {
        //[SerializeField] private GameObject cardContainer;
        [SerializeField] private List<GameObject> cardContainers;
        [SerializeField] private BingoCard bingoCard;
        [SerializeField] private List<Toggle> cardSelectionButton;

        private readonly Dictionary<int, BingoCard> bingoCards = new Dictionary<int, BingoCard>();

        private Dictionary<int, List<int>> _itemPatterns = new Dictionary<int, List<int>>();
        

        private void AddListeners(bool canEnable)
        {
            if (canEnable)
            {
                EventManager.onBallRollOutEvent += CheckForAutoDaubs;
                EventManager.onRestartGameButtonEvent += ResetGame;
            }
            else
            {
                EventManager.onBallRollOutEvent -= CheckForAutoDaubs;
                EventManager.onRestartGameButtonEvent -= ResetGame;
            }
        }

        private void Start()
        {

            for (var i = 0; i < GameConfigs.GameConfigData.ItemPattern.Count; i++)
            {
                var patternString = GameConfigs.GameConfigData.ItemPattern[i];
                _itemPatterns.Add(i, patternString.Split(",").Select(int.Parse).ToList());

            }
            ResetGame();
        }

        private void AttachCardOnScreen()
        {
            bingoCards.Clear();
            SetCardVisibility(0);
            cardSelectionButton[0].isOn = true;
            for (var i = 0; i < GameConfigs.NumberOfCardToPlay; i++)
            {
                var card = Instantiate(bingoCard, GetParent(i).gameObject.transform);
                card.SetData(i, _itemPatterns[Random.Range(0,_itemPatterns.Count)]);
                bingoCards.Add(i, card);
            }

           
        }

        public void SetCardVisibility(int showCardIndex)
        {
            //if(showCardIndex+1>GameConfigs.NumberOfCardToPlay) return;
            for (var i = 0; i < cardContainers.Count; i++)
            {
                cardSelectionButton[i].interactable = false;
                cardContainers[i].SetActive(false);
            }

            for (int j = 0; j < GameConfigs.NumberOfCardToPlay/2; j++)
            {
                cardSelectionButton[j].interactable = true;
            }
            
            cardContainers[showCardIndex].SetActive(true);
        }
        private GameObject GetParent(int cardId)
        {
            var gameObj =  cardContainers[3];
            switch (cardId)
            {
                case 0:
                case 1:
                    gameObj =  cardContainers[0];
                    break;
                case 2:
                case 3:
                    gameObj = cardContainers[1];
                    break;
                case 4:
                case 5:
                    gameObj = cardContainers[2];
                    break;
                    
            }

            return gameObj;
        }

        private void CheckForAutoDaubs(List<int> calledBalls)
        {
            if (!GameConfigs.IsAutoDaubEnable) return;

            foreach (var bingoCard in bingoCards)
            {
                bingoCard.Value.DoAutoDaub(calledBalls);
            }

            CheckForAllCardGameEnd();
        }

        private void CheckForAllCardGameEnd()
        {
            var cardDaubCompleteCount = 0;
            foreach (var bingoCard in bingoCards)
            {
                if (bingoCard.Value.IsAllCellDaubed())
                    cardDaubCompleteCount++;
            }

            if (cardDaubCompleteCount >= bingoCards.Count)
            {
                EventManager.onGameEndEvent?.Invoke();
            }
        }

        private void ResetGame()
        {
            foreach (var cardContainerParent in cardContainers)
            {
                foreach (Transform child in cardContainerParent.transform)
                {
                    Destroy(child.gameObject);
                }
                
            }

            GameSummary.ResetData();
            Utils.ResetGameData();
            AttachCardOnScreen();
        }

        private void OnEnable()
        {
            AddListeners(true);
        }

        private void OnDisable()
        {
            AddListeners(false);
        }
    }
}