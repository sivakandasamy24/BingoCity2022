using UnityEngine;

namespace BingoCity
{
    [CreateAssetMenu(menuName = "GameConfigData", fileName = "GameConfigData", order = 0)]
    public class GameConfigData : ScriptableObject
    {
        [SerializeField] public int maxNumberOfBallPerClick = 2;

        public int MaxNumberOfBallPerClick => maxNumberOfBallPerClick;
    }
}