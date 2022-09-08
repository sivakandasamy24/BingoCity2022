using UnityEngine;
using UnityEngine.SceneManagement;

namespace BingoCity
{
    public class UIManager:MonoBehaviour
    {
        [SerializeField] private GameConfigData gameConfigData;

        public GameConfigData GameConfigData => gameConfigData;
        public void OnResetGameButton()
        {
            EventManager.OnRestartGameButtonEvent();
        }

        public void OnGetNextBallButton()
        {
            EventManager.OnGetNextBallButtonEvent();
        }

        public void OnBackButton(int loadSceneIndex)
        {
            SceneManager.LoadScene(loadSceneIndex);
        }
    }
}