using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.scorejam18.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        public void OnClick_Play()
        {
            int level = Random.Range(1, 4);
            SceneManager.LoadScene("Level" + level);
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