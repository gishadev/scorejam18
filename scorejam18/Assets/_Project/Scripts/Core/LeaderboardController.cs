using System.Collections;
using LootLocker.Requests;
using UnityEngine;

namespace Gisha.scorejam18.Core
{
    public class LeaderboardController : MonoBehaviour
    {
        public static LeaderboardController Instance { get; private set; }

        public string[] PlayerNames { get; private set; } = new string[15];
        public int[] PlayerScores { get; private set; } = new int[15];

        const int LeaderboardID = 2689;

        private void Awake()
        {
            CreateInstance();
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

        public IEnumerator SubmitScoreRoutine(int score)
        {
            bool done = false;
            LootLockerSDKManager.SubmitScore(PlayerManager.PlayerID, score, LeaderboardID, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Successful");
                    done = true;
                }
                else
                {
                    Debug.Log("failed: " + response.Error);
                    done = true;
                }
            });
            yield return new WaitWhile(() => done == false);
        }

        public IEnumerator FetchTopHighScoresRoutine()
        {
            bool done = false;
            LootLockerSDKManager.GetScoreListMain(LeaderboardID, 15, 0, (response) =>
            {
                if (response.success)
                {
                    LootLockerLeaderboardMember[] members = response.items;

                    for (int i = 0; i < members.Length; i++)
                    {
                        if (members[i].player.name != "")
                            PlayerNames[14 - i] = members[i].player.name;
                        else
                            PlayerNames[14 - i] = members[i].player.id.ToString();

                        PlayerScores[14 - i] = members[i].score;
                    }

                    done = true;
                }
                else
                {
                    Debug.Log("Failed " + response.Error);
                    done = true;
                }
            });

            yield return new WaitWhile(() => done == false);
        }
    }
}