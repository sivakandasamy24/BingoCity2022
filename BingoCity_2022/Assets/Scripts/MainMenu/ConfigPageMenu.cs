using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BingoCity
{
    public class ConfigPageMenu:MonoBehaviour
    {
        [SerializeField] private TMP_InputField cardSpanCount;
        [SerializeField] private TMP_InputField maxNumberRoll;
        [SerializeField] private TMP_InputField maxNumberOfBallPerClick;
        [SerializeField] private TMP_InputField buyAdditionalRollCount;
        [SerializeField] private TMP_InputField timerDuration;
        public void LoadScene(int loadSceneIndex)
        {
            GameConfigs.LoadDebugConfigPage = true;
            GameConfigs.cardSpanCount = int.Parse(cardSpanCount.text);
            GameConfigs.maxNumberRoll = int.Parse(maxNumberRoll.text);
            GameConfigs.maxNumberOfBallPerClick = int.Parse(maxNumberOfBallPerClick.text);
            GameConfigs.buyAdditionalRollCount = int.Parse(buyAdditionalRollCount.text);
            GameConfigs.timerDuration = int.Parse(timerDuration.text);

            SceneManager.LoadScene(loadSceneIndex);
        }
    }
}