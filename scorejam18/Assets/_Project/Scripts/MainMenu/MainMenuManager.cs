using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.scorejam18.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        public void OnClick_Play()
        {
            SceneManager.LoadScene("Game");
        }

        public void OnClick_Settings()
        {
        }

        public void OnClick_Exit()
        {
            Application.Quit();
        }
    }
}