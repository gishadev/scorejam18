using System.Collections;
using LootLocker.Requests;
using UnityEngine;

namespace Gisha.scorejam18.Core
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }

        public static string PlayerID => PlayerPrefs.GetString("PlayerID");
        public static int CurrentScore;
        
        private void Awake()
        {
            CreateInstance();
        }

        private void Start()
        {
            StartCoroutine(LoginRoutine());
        }

        private void CreateInstance()
        {
            DontDestroyOnLoad(gameObject);

            if (Instance == null)
                Instance = this;
            else
            {
                if (Instance != this)
                    Destroy(gameObject);
            }
        }

        private IEnumerator LoginRoutine()
        {
            bool done = false;
            LootLockerSDKManager.StartGuestSession((response) =>
            {
                if (response.success)
                {
                    Debug.Log("Player was logged in");
                    PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                    done = true;
                }
                else
                {
                    Debug.Log("Could not start session " + response.Error);
                    done = true;
                }
            });
            yield return new WaitWhile(() => done == false);
        }
    }
}