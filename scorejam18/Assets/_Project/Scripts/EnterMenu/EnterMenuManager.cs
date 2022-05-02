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
            if (!string.IsNullOrEmpty(companyInput.text))
            {
               // PlayerPrefs.SetString("PlayerID", companyInput.text);
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}