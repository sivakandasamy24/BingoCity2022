using System.Collections.Generic;
using UnityEngine;

namespace BingoCity
{
    [CreateAssetMenu(menuName = "GameConfigData", fileName = "GameConfigData", order = 0)]
    public class GameConfigData : ScriptableObject
    {
        [SerializeField] private List<string> itemPattern;
        [SerializeField] private int maxNumberOfBallPerClick = 2; //GS ball rollout on each click
        [SerializeField] private int maxNumberRoll = 5; //GS max click on round starts
        [SerializeField] private int buyPopupMaxWindowCount = 1; //GS additional click buy count
        [SerializeField] private int buyPopupAutoTimer = 1; // GS autoTimerWaiting timerIn sec
        [SerializeField] private int buyAdditionalRollCount = 2; //GS additional click buy count

        public int MaxNumberRoll => maxNumberRoll;
        public int BuyPopupMaxWindowCount => buyPopupMaxWindowCount;
        public int BuyAdditionalRollCount => buyAdditionalRollCount;
        public List<string> ItemPattern => itemPattern;
        public int MaxNumberOfBallPerClick => maxNumberOfBallPerClick;
        public int BuyPopupAutoTimer => buyPopupAutoTimer;
    }
}