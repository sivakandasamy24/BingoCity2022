using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BingoCity
{
    public class BingoBall : MonoBehaviour
    {
        [SerializeField] private Text ballNumber;
        [SerializeField] private Image bImage;
        [SerializeField] private Image iImage;
        [SerializeField] private Image nImage;
        [SerializeField] private Image gImage;
        [SerializeField] private Image oImage;

        public void SetData(int calledBallNumber)
        {
            var bingoLetter = Utils.GetLetterByValue(calledBallNumber);
            ballNumber.text = $"{calledBallNumber}";
            ResetAllImages();
            switch (bingoLetter)
            {
                case GameConfigs.BingoLetters.BRow:
                    bImage.gameObject.SetActive(true);
                    break;
                case GameConfigs.BingoLetters.IRow:
                    iImage.gameObject.SetActive(true);
                    break;
                case GameConfigs.BingoLetters.NRow:
                    nImage.gameObject.SetActive(true);
                    break;
                case GameConfigs.BingoLetters.GRow:
                    gImage.gameObject.SetActive(true);
                    break;
                case GameConfigs.BingoLetters.ORow:
                    oImage.gameObject.SetActive(true);
                    break;
            }
        }

        private void ResetAllImages()
        {
            bImage.gameObject.SetActive(false);
            iImage.gameObject.SetActive(false);
            nImage.gameObject.SetActive(false);
            gImage.gameObject.SetActive(false);
            oImage.gameObject.SetActive(false);
        }
    }
}