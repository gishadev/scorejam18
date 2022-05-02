using System;
using Gisha.Effects.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Gisha.scorejam18.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        private void Start()
        {
            int randomMusic = Random.Range(1, 3);
            AudioManager.Instance.PlayMusic("MenuMusic" + randomMusic);
        }

        public void OnClick_Play()
        {
            int level = Random.Range(1, 4);
            SceneManager.LoadScene("Level" + level);
            
            AudioManager.PlayClick();
        }

        public void OnClick_Settings()
        {
            AudioManager.PlayClick();
        }

        public void OnClick_Exit()
        {
            Application.Quit();
            
            AudioManager.PlayClick();
        }
    }
}