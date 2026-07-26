using System.Collections.Generic;

// Sorts leaderboard entries by enemies defeated, descending (highest first).

public static class LeaderboardSorter
{
    public static List<LeaderboardEntry> SortByScoreDescending(List<LeaderboardEntry> entries)
    {
        List<LeaderboardEntry> sorted = new List<LeaderboardEntry>(entries);

        for (int i = 1; i < sorted.Count; i++)
        {
            LeaderboardEntry current = sorted[i];
            int j = i - 1;

            while (j >= 0 && sorted[j].enemiesDefeated < current.enemiesDefeated)
            {
                sorted[j + 1] = sorted[j];
                j--;
            }
            sorted[j + 1] = current;
        }

        return sorted;
    }
}