using System.Collections;
using System.Collections.Generic;

using System;
using UnityEngine;

namespace BingoCity
{
    public class EventManager : MonoBehaviour
    {
        public static Action<List<int>> BallRollOutEvent;
        
        public static Action OnRestartGameButtonEvent;
        public static Action OnGetNextBallButtonEvent;
    }
}