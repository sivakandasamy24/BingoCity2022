using System;
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
        [SerializeField] private int buyAdditionalRollCost = 30; //GS additional click buy count
        [SerializeField] private int cardSpanCount = 60; //GS additional click buy count
        [SerializeField] private int timerDuration = 10; //GS Timer to end the game
        [SerializeField] private int raidTokenCapCount =2; //GS conversion from tot bingo to raidToken
        [SerializeField] private List<int> coinReward ; //GS reward count
        
        [SerializeField] private List<AudioTracksData> audioTracks;


        public AudioTracksData GetAudioFileData(AudioTrackNames soundName)
        {
           return  audioTracks.Find(x => x.audioName == soundName);
        }
       
        public List<int> CoinReward => coinReward;
        public List<AudioTracksData> AudioTracks => audioTracks;
        public int RaidTokenCapCount => raidTokenCapCount;
        public int BuyPopupMaxWindowCount => buyPopupMaxWindowCount;
        public List<string> ItemPattern => itemPattern;
        public int BuyPopupAutoTimer => buyPopupAutoTimer;
        public int BuyAdditionalRollCost => buyAdditionalRollCost;
        
        public int TimerDuration => GameConfigs.LoadDebugConfigPage?GameConfigs.timerDuration:timerDuration;
        public int BuyAdditionalRollCount => GameConfigs.LoadDebugConfigPage?GameConfigs.buyAdditionalRollCount:buyAdditionalRollCount;
        public int CardSpanCount => GameConfigs.LoadDebugConfigPage?GameConfigs.cardSpanCount:cardSpanCount;
        public int MaxNumberRoll => GameConfigs.LoadDebugConfigPage?GameConfigs.maxNumberRoll:maxNumberRoll;
        public int MaxNumberOfBallPerClick => GameConfigs.LoadDebugConfigPage?GameConfigs.maxNumberOfBallPerClick:maxNumberOfBallPerClick;
    }
    
    public enum AudioTrackNames
    {
        GsDaub ,
        GsItemReveal ,
        GsPopupAppear ,
        GsClick ,
        GsPopupClose ,
        GsRoundOver ,
        GsBingo  ,
        GsSummaryPopup,
        DefaultFly,
        DefaultDamage,
        BombFly,
        BombDamage,
        RocketFly,
        RocketDamage
    }
    
    [Serializable]
    public class AudioTracksData
    {
        public AudioTrackNames audioName;
        public AudioClip trackFile;
    }
}