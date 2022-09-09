using System.Collections;
using System.Collections.Generic;

using System;
using UnityEngine;

namespace BingoCity
{
    public class EventManager : MonoBehaviour
    {
        public static Action<List<int>> onBallRollOutEvent;
        public static Action onBingoEvent;
        public static Action onGameEndEvent;
        
        public static Action onRestartGameButtonEvent;
        public static Action onGetNextBallButtonEvent;
    }
}