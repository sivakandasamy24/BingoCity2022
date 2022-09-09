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
        [SerializeField] private int cardSpanCount = 60; //GS additional click buy count
        [SerializeField] private int timerDuration = 10; //GS Timer to end the game
        
        [SerializeField] private int raidTokenCapCount =2; //GS conversion from tot bingo to raidToken
        [SerializeField] private List<int> coinReward ; //GS reward count
        

       
        public List<int> CoinReward => coinReward;
        public int TimerDuration => timerDuration;
        public int RaidTokenCapCount => raidTokenCapCount;
        public int BuyPopupMaxWindowCount => buyPopupMaxWindowCount;
        public List<string> ItemPattern => itemPattern;
        public int BuyPopupAutoTimer => buyPopupAutoTimer;
        
        public int BuyAdditionalRollCount => GameConfigs.LoadDebugConfigPage?GameConfigs.buyAdditionalRollCount:buyAdditionalRollCount;
        public int CardSpanCount => GameConfigs.LoadDebugConfigPage?GameConfigs.cardSpanCount:cardSpanCount;
        public int MaxNumberRoll => GameConfigs.LoadDebugConfigPage?GameConfigs.maxNumberRoll:maxNumberRoll;
        public int MaxNumberOfBallPerClick => GameConfigs.LoadDebugConfigPage?GameConfigs.maxNumberOfBallPerClick:maxNumberOfBallPerClick;
    }
}