using UnityEngine;
using TMPro;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusText;

    public void OnSubmitScoreClicked()
    {
        statusText.text = "Submitting...";

        int defeated = SaveManager.Instance.CurrentSave.playerStats.totalEnemiesDefeated;
        int deaths = SaveManager.Instance.CurrentSave.playerStats.totalDeaths;

        LeaderboardService.Instance.SubmitScore("Player", defeated, deaths, (success) =>
        {
            statusText.text = success ? "Score submitted!" : "Failed to submit, offline?";
        });
    }
}