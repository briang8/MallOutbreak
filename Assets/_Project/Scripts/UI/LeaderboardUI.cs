using UnityEngine;
using TMPro;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private TextMeshProUGUI leaderboardListText;
    [SerializeField] private TMP_InputField nameInputField;

    public void OnSubmitScoreClicked()
    {
        statusText.text = "Submitting...";

        string playerName = string.IsNullOrWhiteSpace(nameInputField.text) ? "Player" : nameInputField.text;
        int defeated = SaveManager.Instance.CurrentSave.playerStats.totalEnemiesDefeated;
        int deaths = SaveManager.Instance.CurrentSave.playerStats.totalDeaths;

        LeaderboardService.Instance.SubmitScore(playerName, defeated, deaths, (success) =>
        {
            statusText.text = success ? "Score submitted!" : "Failed to submit — offline?";
            if (success) RefreshLeaderboardDisplay();
        });
    }

    public void RefreshLeaderboardDisplay()
    {
        LeaderboardService.Instance.FetchLeaderboard((data, success) =>
        {
            if (!success)
            {
                leaderboardListText.text = "Could not load leaderboard";
                return;
            }

            var sorted = LeaderboardSorter.SortByScoreDescending(data.entries);
            string display = "";
            for (int i = 0; i < sorted.Count && i < 3; i++) // top 3 only
            {
                display += (i + 1) + ". " + sorted[i].playerName + "-" + sorted[i].enemiesDefeated + " kills\n";
            }
            leaderboardListText.text = display;
        });
    }
}