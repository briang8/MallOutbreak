using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;

// Handles all communication with the jsonbin.io leaderboard endpoint.
// Every call goes through UnityWebRequest with explicit success/failure
// branches — no assumption the network is available. Callers (UI) get
// notified either way via callback, never left hanging on a silent failure.
public class LeaderboardService : MonoBehaviour
{
    public static LeaderboardService Instance { get; private set; }

    private const string BinId = "6a64d2abda38895dfe8e9791";
    private const string ApiKey = "$2a$10$HuDQEBr8h1k5N7Hd2c.4eufvcb4guT1FI39PvnClRtxwlDaKHP1nO";
    private const string BaseUrl = "https://api.jsonbin.io/v3/b/6a64d2abda38895dfe8e9791";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SubmitScore(string playerName, int enemiesDefeated, int deaths, System.Action<bool> onComplete)
    {
        StartCoroutine(SubmitScoreRoutine(playerName, enemiesDefeated, deaths, onComplete));
    }

    private IEnumerator SubmitScoreRoutine(string playerName, int enemiesDefeated, int deaths, System.Action<bool> onComplete)
    {
        // Fetch current leaderboard first, append, then push the whole thing back —
        // jsonbin.io's free tier works on whole-document read/write, not partial updates
        yield return FetchLeaderboardRoutine((data, success) =>
        {
            if (!success)
            {
                Debug.LogWarning("Could not fetch leaderboard, skipping submit");
                onComplete?.Invoke(false);
                return;
            }

            data.entries.Add(new LeaderboardEntry
            {
                playerName = playerName,
                enemiesDefeated = enemiesDefeated,
                deaths = deaths
            });

            StartCoroutine(PushLeaderboardRoutine(data, onComplete));
        });
    }

    private IEnumerator PushLeaderboardRoutine(LeaderboardData data, System.Action<bool> onComplete)
    {
        string json = JsonConvert.SerializeObject(data);
        UnityWebRequest request = new UnityWebRequest(BaseUrl + BinId, "PUT");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("X-Master-Key", ApiKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Leaderboard updated successfully");
            onComplete?.Invoke(true);
        }
        else
        {
            Debug.LogWarning("Failed to update leaderboard: " + request.error);
            onComplete?.Invoke(false);
        }
    }

    public void FetchLeaderboard(System.Action<LeaderboardData, bool> onComplete)
    {
        StartCoroutine(FetchLeaderboardRoutine(onComplete));
    }

    private IEnumerator FetchLeaderboardRoutine(System.Action<LeaderboardData, bool> onComplete)
    {
        UnityWebRequest request = UnityWebRequest.Get(BaseUrl + BinId + "/latest");
        request.SetRequestHeader("X-Master-Key", ApiKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            try
            {
                string json = request.downloadHandler.text;
                // jsonbin.io wraps the actual content inside a "record" field
                var wrapper = JsonConvert.DeserializeObject<JsonBinWrapper>(json);
                onComplete?.Invoke(wrapper.record, true);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Failed to parse leaderboard response: " + e.Message);
                onComplete?.Invoke(new LeaderboardData(), false);
            }
        }
        else
        {
            Debug.LogWarning("Failed to fetch leaderboard: " + request.error);
            onComplete?.Invoke(new LeaderboardData(), false);
        }
    }
}

[System.Serializable]
public class JsonBinWrapper
{
    public LeaderboardData record;
}