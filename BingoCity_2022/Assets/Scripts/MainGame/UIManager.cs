using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BingoCity
{
    public class UIManager:MonoBehaviour
    {
        [SerializeField] private GameConfigData gameConfigData;
        [SerializeField] private TextMeshProUGUI rollCountText;
        [SerializeField] private Button rollButton;

        [SerializeField] private GameObject popupParent;
        [SerializeField] private GameObject buyRollPopup;
        [SerializeField] private GameObject summaryPopup;


        private int _currentRollCount;
        private int _autoPopupShowCount;
        public GameConfigData GameConfigData => gameConfigData;

        private void Awake()
        {
            Utils.ballCallingSpan = gameConfigData.CardSpanCount;
        }

        private void Start()
        {
            
            InitData();
        }

        private void InitData()
        {
            _currentRollCount = gameConfigData.MaxNumberRoll;
            _autoPopupShowCount = gameConfigData.BuyPopupMaxWindowCount;
            popupParent.gameObject.SetActive(false);
            buyRollPopup.gameObject.SetActive(false);
            summaryPopup.gameObject.SetActive(false);
            UpdateRollText();
        }

        private void UpdateRollText()
        {
            rollCountText.text = $" {_currentRollCount}";
            CheckAndEnableRollButton();
        }

        private void CheckAndShowAutoPopup()
        {
            if (_autoPopupShowCount > 0)
            {
                //startTimerToShowBuyPopup
                Invoke(nameof(ShowBuyRollCountPopup),gameConfigData.BuyPopupAutoTimer);
            }
            else
            {
                ShowGameSummaryPopup();
            }
        }

        private void ShowGameSummaryPopup()
        {
            popupParent.gameObject.SetActive(true);
            summaryPopup.SetActive(true);
        }
        private void ShowBuyRollCountPopup()
        {
            popupParent.gameObject.SetActive(true);
            buyRollPopup.SetActive(true);
        }

        private void HideBuyRollCountPopup()
        {
            popupParent.gameObject.SetActive(false);
            buyRollPopup.SetActive(false);
        }
        

        private void CheckAndEnableRollButton()
        {
            if (_currentRollCount<1)
            {
                //disable Roll button
                rollButton.interactable = false;
                CheckAndShowAutoPopup();
            }
            else
            {
                rollButton.interactable = true;
            }
        }

        public void OnResetGameButton()
        {
            InitData();
            EventManager.OnRestartGameButtonEvent();
        }

        public void OnGetNextBallButton()
        {
            _currentRollCount--;
            UpdateRollText();
            
            EventManager.OnGetNextBallButtonEvent();
        }

        public void OnBackButton(int loadSceneIndex)
        {
            SceneManager.LoadScene(loadSceneIndex);
        }

        public void OnBuyPopupYes(bool isBuying)
        {
            HideBuyRollCountPopup();
            if (isBuying)
            {
                _autoPopupShowCount--;
                _currentRollCount += gameConfigData.BuyAdditionalRollCount;
                UpdateRollText();
            }
            else
            {
                ShowGameSummaryPopup();
            }
        }
    }
}