using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BingoCity
{
    public class BingoCardManager : MonoBehaviour
    {
        [SerializeField] private GameObject cardContainer;
        [SerializeField] private BingoCard bingoCard;

        private readonly Dictionary<int, BingoCard> bingoCards = new Dictionary<int, BingoCard>();

        private Dictionary<int, List<int>> _itemPatterns = new Dictionary<int, List<int>>();
        
        private UIManager _uiManager;

        private void AddListeners(bool canEnable)
        {
            if (canEnable)
            {
                EventManager.BallRollOutEvent += CheckForAutoDaubs;
                EventManager.OnRestartGameButtonEvent += ResetGame;
            }
            else
            {
                EventManager.BallRollOutEvent -= CheckForAutoDaubs;
                EventManager.OnRestartGameButtonEvent -= ResetGame;
            }
        }

        private void Start()
        {
            _uiManager = GetComponent<UIManager>();
            
            for (var i = 0; i < _uiManager.GameConfigData.ItemPattern.Count; i++)
            {
                var patternString = _uiManager.GameConfigData.ItemPattern[i];
                _itemPatterns.Add(i, patternString.Split(",").Select(int.Parse).ToList());

            }
            ResetGame();
        }

        private void AttachCardOnScreen()
        {
            bingoCards.Clear();
            for (var i = 0; i < GameConfigs.NumberOfCardToPlay; i++)
            {
                var card = Instantiate(bingoCard, cardContainer.gameObject.transform);
                card.SetData(i, _itemPatterns[Random.Range(0,_itemPatterns.Count)]);
                bingoCards.Add(i, card);
            }
        }

        private void CheckForAutoDaubs(List<int> calledBalls)
        {
            if (!GameConfigs.IsAutoDaubEnable) return;

            foreach (var bingoCard in bingoCards)
            {
                bingoCard.Value.DoAutoDaub(calledBalls);
            }
        }

        private void ResetGame()
        {
            foreach (Transform child in cardContainer.transform)
            {
                Destroy(child.gameObject);
            }

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