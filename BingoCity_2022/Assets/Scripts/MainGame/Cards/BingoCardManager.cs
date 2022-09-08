using System.Collections.Generic;
using UnityEngine;

namespace BingoCity
{
    public class BingoCardManager : MonoBehaviour
    {
        [SerializeField] private GameObject cardContainer;
        [SerializeField] private BingoCard bingoCard;

        private readonly Dictionary<int, BingoCard> bingoCards = new Dictionary<int, BingoCard>();

        private List<int> _itemPatternList1 = new List<int>()
        {
            0, 0, 0, 3, 0,
            1, 0, 0, 3, 0,
            0, 0, 0, 0, 0,
            0, 5, 5, 0, 0,
            0, 5, 5, 0, 0,
        };

        private List<int> _itemPatternList2 = new List<int>()
        {
            0, 0, 2, 0, 0,
            3, 0, 0, 0, 0,
            3, 0, 0, 0, 0,
            0, 0, 0, 4, 4,
            0, 0, 0, 0, 0,
        };

        private Dictionary<int, List<int>> _itemPatterns = new Dictionary<int, List<int>>();

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
            _itemPatterns.Add(0, _itemPatternList1);
            _itemPatterns.Add(1, _itemPatternList2);
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