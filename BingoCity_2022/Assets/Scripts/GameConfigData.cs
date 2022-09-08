using System.Collections.Generic;
using UnityEngine;

namespace BingoCity
{
    [CreateAssetMenu(menuName = "GameConfigData", fileName = "GameConfigData", order = 0)]
    public class GameConfigData : ScriptableObject
    {
        [SerializeField] private int maxNumberOfBallPerClick = 2;
        [SerializeField] private List<string> itemPattern;

        public List<string> ItemPattern => itemPattern;
        public int MaxNumberOfBallPerClick => maxNumberOfBallPerClick;
        
        
    }
}