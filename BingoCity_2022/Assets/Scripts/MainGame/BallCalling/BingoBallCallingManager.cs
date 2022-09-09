using System;
using System.Collections.Generic;
using UnityEngine;

namespace BingoCity
{
    public class BingoBallCallingManager : MonoBehaviour
    {
        [SerializeField] private GameObject bingoBallHolderParent;
        [SerializeField] private BingoBall bingoBallPrefabs;


        

        private void Start()
        {
            ResetBallPanels();
        }

        private void OnEnable()
        {
            AddListeners(true);
        }

        private void OnDisable()
        {
            AddListeners(false);
        }
        
        private void AddListeners(bool canEnable)
        {
            if (canEnable)
            {
                EventManager.onGetNextBallButtonEvent += GetNextBall;
                EventManager.onRestartGameButtonEvent += ResetBallPanels;
            }
            else
            {
                EventManager.onGetNextBallButtonEvent -= GetNextBall;
                EventManager.onRestartGameButtonEvent -= ResetBallPanels;
            }
        }

        private void GetNextBall()
        {
            var calledBalls = new List<int>();
            for (var i = 0; i < GameConfigs.GameConfigData.MaxNumberOfBallPerClick; i++)
            {
                calledBalls.Add(Utils.GetRandUnCalledBallNumber());
            }

            ShowBallOnPanel(calledBalls);
            print($"--siva--generatedBalls{string.Join(",", calledBalls)}");
            EventManager.onBallRollOutEvent?.Invoke(calledBalls);
        }

        private void ShowBallOnPanel(List<int> calledBalls)
        {
            ClearExistingBalls();
            foreach (var calledBallnumber in calledBalls)
            {
                var bingoBall = Instantiate(bingoBallPrefabs, bingoBallHolderParent.transform);
                bingoBall.SetData(calledBallnumber);
            }
        }

        private void ClearExistingBalls()
        {
            foreach (Transform child in bingoBallHolderParent.transform) {
                Destroy(child.gameObject);
            }
        }

        private void ResetBallPanels()
        {
            ClearExistingBalls();
        }
    }
}