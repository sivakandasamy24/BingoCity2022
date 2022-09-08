using System;
using System.Collections.Generic;
using UnityEngine;

namespace BingoCity
{
    public class BingoBallCallingManager : MonoBehaviour
    {
        [SerializeField] private GameObject bingoBallHolderParent;
        [SerializeField] private BingoBall bingoBallPrefabs;


        private UIManager _uiManager;

        private void Start()
        {
            ResetBallPanels();
            _uiManager = GetComponent<UIManager>();
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
                EventManager.OnGetNextBallButtonEvent += GetNextBall;
                EventManager.OnRestartGameButtonEvent += ResetBallPanels;
            }
            else
            {
                EventManager.OnGetNextBallButtonEvent -= GetNextBall;
                EventManager.OnRestartGameButtonEvent -= ResetBallPanels;
            }
        }

        private void GetNextBall()
        {
            var calledBalls = new List<int>();
            for (var i = 0; i < _uiManager.GameConfigData.MaxNumberOfBallPerClick; i++)
            {
                calledBalls.Add(Utils.GetRandUnCalledBallNumber());
            }

            ShowBallOnPanel(calledBalls);
            print($"--siva--generatedBalls{string.Join(",", calledBalls)}");
            EventManager.BallRollOutEvent?.Invoke(calledBalls);
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