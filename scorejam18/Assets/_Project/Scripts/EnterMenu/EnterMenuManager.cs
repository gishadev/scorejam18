using Gisha.Effects.Audio;
using LootLocker.Requests;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.scorejam18.EnterMenu
{
    public class EnterMenuManager : MonoBehaviour
    {
        [SerializeField] private TMP_InputField companyInput;

        public void OnClick_Enter()
        {
            LootLockerSDKManager.SetPlayerName(companyInput.text, (response) =>
            {
                if (response.success)
                    Debug.Log("Name was changed!");
                else
                    Debug.Log("Name changing failed! " + response.Error);
            });

            SceneManager.LoadScene("MainMenu");
            
            AudioManager.PlayClick();
        }
    }
}