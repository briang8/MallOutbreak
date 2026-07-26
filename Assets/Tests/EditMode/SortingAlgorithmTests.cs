using NUnit.Framework;
using System.Collections.Generic;

public class SortingAlgorithmTests
{
    [Test]
    public void SortByScoreDescending_OrdersHighestFirst()
    {
        List<LeaderboardEntry> entries = new List<LeaderboardEntry>
        {
            new LeaderboardEntry { playerName = "A", enemiesDefeated = 5 },
            new LeaderboardEntry { playerName = "B", enemiesDefeated = 20 },
            new LeaderboardEntry { playerName = "C", enemiesDefeated = 10 }
        };

        List<LeaderboardEntry> sorted = LeaderboardSorter.SortByScoreDescending(entries);

        Assert.AreEqual("B", sorted[0].playerName);
        Assert.AreEqual("C", sorted[1].playerName);
        Assert.AreEqual("A", sorted[2].playerName);
    }

    [Test]
    public void SortByScoreDescending_HandlesEmptyList()
    {
        List<LeaderboardEntry> entries = new List<LeaderboardEntry>();

        List<LeaderboardEntry> sorted = LeaderboardSorter.SortByScoreDescending(entries);

        Assert.AreEqual(0, sorted.Count);
    }
}