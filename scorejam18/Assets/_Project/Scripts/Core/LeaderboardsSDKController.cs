using LootLocker.Requests;
using UnityEngine;

namespace Gisha.scorejam18.Core
{
    public static class LeaderboardsSDKController
    {
        const int LeaderboardID = 2689;

        public static void SubmitScore(string nickname, int score)
        {
            LootLockerSDKManager.SubmitScore(nickname, score, LeaderboardID, (response) =>
            {
                if (response.statusCode == 200)
                    Debug.Log("Successful");
                else
                    Debug.Log("failed: " + response.Error);
            });
        }
    }
}