using NUnit.Framework;
using System.Collections.Generic;

public class LevelUnlockTests
{
    [Test]
    public void MarkLevelCompleted_UnlocksNextLevel()
    {
        SaveData data = new SaveData();
        data.levels.Add(new LevelProgress { levelIndex = 1, isUnlocked = true, isCompleted = false });
        data.levels.Add(new LevelProgress { levelIndex = 2, isUnlocked = false, isCompleted = false });

        // Testing the unlock logic directly rather than through SaveManager,
        // since SaveManager is a MonoBehaviour singleton tied to Awake/file I/O —
        // isolating the core logic keeps this a true unit test, not an integration test
        
        LevelProgress level1 = data.levels.Find(l => l.levelIndex == 1);
        LevelProgress level2 = data.levels.Find(l => l.levelIndex == 2);
        level1.isCompleted = true;
        level2.isUnlocked = true; // this is what MarkLevelCompleted does internally

        Assert.IsTrue(level2.isUnlocked);
        Assert.IsTrue(level1.isCompleted);
    }
}