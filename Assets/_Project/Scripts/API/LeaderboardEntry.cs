using System;

[Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public int enemiesDefeated;
    public int deaths;
}

[Serializable]
public class LeaderboardData
{
    public System.Collections.Generic.List<LeaderboardEntry> entries = new System.Collections.Generic.List<LeaderboardEntry>();
}