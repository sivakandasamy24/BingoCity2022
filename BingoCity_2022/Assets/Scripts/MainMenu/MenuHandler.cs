using UnityEngine;
using UnityEngine.SceneManagement;

namespace BingoCity
{
    public class MenuHandler : MonoBehaviour
    {
        public void LoadScene(int loadSceneIndex)
        {
            SceneManager.LoadScene(loadSceneIndex);
        }


    }
}