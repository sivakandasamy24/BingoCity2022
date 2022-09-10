using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BingoCity
{
    public class CircleTimerScript : MonoBehaviour
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private TextMeshProUGUI timerText;

        private float totDuration;
        private float timeRemaining;
        private bool isTimerRunning;

        private Coroutine timerCount;

        private void Start()
        {
            totDuration = GameConfigs.GameConfigData.TimerDuration;
            fillImage.fillAmount = 1f;
            timerText.text = totDuration.ToString();
        }
        

        private IEnumerator TimerCount()
        {
            //AudioManager.instance.StartTimerSound(true);
            float value = 0;

            while (timeRemaining > 1)
            {
                //  Debug.Log("timeRemaing--"+timeRemaing);
                timeRemaining -= Time.deltaTime;

                timerText.text = "" + (int) timeRemaining;
                value = timeRemaining / totDuration;
                fillImage.fillAmount = value;

                if (timeRemaining <= 1)
                {
                    fillImage.fillAmount = 0;
                    StopTimerNow();
                    EventManager.onGameEndEvent?.Invoke();
                    break;
                }

                yield return null;
            }

            yield return null;
           
            
        }
        
        public void StopTimerNow()
        {
           // AudioManager.instance.StartTimerSound(false);
            
            isTimerRunning = false;
            StopCoroutine(timerCount);

        }

        

        private void StartBingoTimer()
        {
            if (!isTimerRunning)
            {
                timeRemaining = totDuration;
                isTimerRunning = true;
                timerCount = StartCoroutine(TimerCount());
            }
        }

        public void RestartGame()
        {
            isTimerRunning = false;
            if(timerCount!=null)
                StopCoroutine(timerCount);
            if (timeRemaining < 0)
                timeRemaining = 0;
            
            totDuration =  GameConfigs.GameConfigData.TimerDuration;
            timerText.text = totDuration.ToString("F0");
            fillImage.fillAmount = 1;
            timeRemaining = totDuration;
            StartBingoTimer();
            // AudioManager.instance.StartTimerSound(false);
        }
    }
}