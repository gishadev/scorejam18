using System;
using Gisha.Effects.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Gisha.scorejam18.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject tutorialScreen;

        private void Start()
        {
            int randomMusic = Random.Range(1, 3);
            AudioManager.Instance.PlayMusic("MenuMusic" + randomMusic);

            if (PlayerPrefs.GetInt("TutorialShowed") == 0)
                tutorialScreen.SetActive(true);
        }

        public void OnClick_Play()
        {
            int level = Random.Range(1, 4);
            SceneManager.LoadScene("Level" + level);

            AudioManager.PlayClick();
        }

        public void OnClick_Tutorial()
        {
            tutorialScreen.SetActive(true);
            PlayerPrefs.SetInt("TutorialShowed", 1);
            AudioManager.PlayClick();
        }

        public void OnClick_CloseTutorial()
        {
            tutorialScreen.SetActive(false);

            AudioManager.PlayClick();
        }

        public void OnClick_Exit()
        {
            Application.Quit();

            AudioManager.PlayClick();
        }
    }
}