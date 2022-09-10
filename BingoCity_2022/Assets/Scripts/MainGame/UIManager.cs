using System;
using System.Collections;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BingoCity
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameConfigData gameConfigData;
        [SerializeField] private  InventoryData inventoryAssetData;
        
        [SerializeField] private TextMeshProUGUI rollCountText;
        [SerializeField] private TextMeshProUGUI raidTokentText;
        [SerializeField] private Button rollButton;
        [SerializeField] private GameObject popupParent;
        [SerializeField] private GameObject buyRollPopup;
        [SerializeField] private TextMeshProUGUI buyRollCountText;
        [SerializeField] private TextMeshProUGUI buyRollCostText;
        [SerializeField] private GameObject summaryPopup;
        [SerializeField] private GameObject roundOver;

        private int _currentRollCount;
        private int _autoPopupShowCount;
        private CircleTimerScript _timerScript;

        private void Awake()
        {
            GameConfigs.GameConfigData = gameConfigData;
            GameConfigs.InventoryAssetData = inventoryAssetData;
            Utils.ballCallingSpan = gameConfigData.CardSpanCount;
            
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
                EventManager.onBingoEvent += UpdateRaidTokenCount;
                EventManager.onGameEndEvent += OnGameEndEvent;
            }
            else
            {
                EventManager.onBingoEvent -= UpdateRaidTokenCount;
                EventManager.onGameEndEvent -= OnGameEndEvent;
            }
        }

        private void Start()
        {
            _timerScript = GetComponent<CircleTimerScript>();
            InitData();
        }
        
        private void InitData()
        {
            _currentRollCount = gameConfigData.MaxNumberRoll;
            _autoPopupShowCount = gameConfigData.BuyPopupMaxWindowCount;
            popupParent.gameObject.SetActive(false);
            buyRollPopup.gameObject.SetActive(false);
            summaryPopup.gameObject.SetActive(false);
            roundOver.gameObject.SetActive(false);
            _timerScript.RestartGame();
            UpdateRollText();
            UpdateRaidTokenCount();
        }

        private void UpdateRaidTokenCount()
        {
            GameSummary.raidTokenGained = GameSummary.bingoGained / gameConfigData.RaidTokenCapCount;
            raidTokentText.text = $"{GameSummary.bingoGained % gameConfigData.RaidTokenCapCount} / {gameConfigData.RaidTokenCapCount}";
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
                StartCoroutine(ShowBuyRollCountPopup());
            }
            else
            {
                StartCoroutine(ShowGameOverPopup());
            }
        }

        private void ShowGameSummaryPopup()
        {
            popupParent.gameObject.SetActive(true);
            buyRollPopup.SetActive(false);
            summaryPopup.SetActive(true);
        }

        private IEnumerator ShowGameOverPopup()
        {
            _timerScript.StopTimerNow();
            rollButton.interactable = false;
            yield return new WaitForSeconds(0.5f); //daubAnimation delay
            
            print($"--time--ShowGameOverPopup {GameConfigs.BingoAnimPlayTime}");
            Observable.ReturnUnit()
                .Delay(TimeSpan.FromSeconds(GameConfigs.BingoAnimPlayTime <= 0 ? 0.75f : GameConfigs.BingoAnimPlayTime))
                .Do(_ => roundOver.SetActive(true))
                .Delay(TimeSpan.FromSeconds(1))
                .Do(_ => ShowGameSummaryPopup())
                .Subscribe()
                .AddTo(this);
        }

        private IEnumerator ShowBuyRollCountPopup()
        {
            yield return new WaitForSeconds(0.5f); ////daubAnimation delay

            print(
                $"--time--ShowBuyRollCountPopup {GameConfigs.BingoAnimPlayTime} -- {gameConfigData.BuyPopupAutoTimer}");

            Observable.ReturnUnit()
                .Delay(TimeSpan.FromSeconds(GameConfigs.BingoAnimPlayTime + gameConfigData.BuyPopupAutoTimer + 1))
                .Do(_ =>
                {
                    print($"--time--ShowBuy ENABLE--");
                    popupParent.gameObject.SetActive(true);
                    buyRollCountText.text = $"Get {gameConfigData.BuyAdditionalRollCount} Extra Rolls";
                    buyRollCostText.text = gameConfigData.BuyAdditionalRollCost.ToString();
                    buyRollPopup.SetActive(true);
                })
                .Subscribe().AddTo(this);
        }

        private void HideBuyRollCountPopup()
        {
            popupParent.gameObject.SetActive(false);
            buyRollPopup.SetActive(false);
        }

        private void CheckAndEnableRollButton()
        {
            if (_currentRollCount < 1)
            {
                //disable Roll button
                rollButton.interactable = false;
                CheckAndShowAutoPopup();
            }
            else
            {
                StartCoroutine(EnableRollButton());
            }
        }

        private IEnumerator EnableRollButton()
        {
            print($"--time--EnableRollButton --IEnumerator--{GameConfigs.BingoAnimPlayTime}");
            yield return new WaitForSeconds(0.5f); //daubAnimation delay

            print($"--time--EnableRollButton {GameConfigs.BingoAnimPlayTime}");
            Observable.ReturnUnit()
                .Delay(TimeSpan.FromSeconds(GameConfigs.BingoAnimPlayTime <= 0
                    ? 0.2f
                    : GameConfigs.BingoAnimPlayTime))
                .Do(_ => rollButton.interactable = true)
                .Subscribe().AddTo(this);
        }

        public void OnResetGameButton()
        {
            EventManager.onRestartGameButtonEvent?.Invoke();
            InitData();
        }

        public void OnGetNextBallButton()
        {
            _currentRollCount--;
            rollButton.interactable = false;
            UpdateRollText();

            EventManager.onGetNextBallButtonEvent?.Invoke();
        }

        public void OnSummaryExitButton(int loadSceneIndex)
        {
            GameSummary.UpdateRewardsToUserAccount();
            OnBackButton(loadSceneIndex);
        }
        public void OnBackButton(int loadSceneIndex)
        {
            GameConfigs.LoadDebugConfigPage = false;
            GameConfigs.NumberOfCardToPlay = 2;
            SceneManager.LoadScene(loadSceneIndex);
        }

        public void OnBuyPopupYes(bool isBuying)
        {
            HideBuyRollCountPopup();
            if (isBuying)
            {
                _autoPopupShowCount--;
                UserInventoryData.UserChips -= gameConfigData.BuyAdditionalRollCost;
                _currentRollCount += gameConfigData.BuyAdditionalRollCount;
                UpdateRollText();
            }
            else
            {
                StartCoroutine(ShowGameOverPopup());
            }
        }

        private void OnGameEndEvent()
        {
            StartCoroutine(ShowGameOverPopup());
        }
    }
}