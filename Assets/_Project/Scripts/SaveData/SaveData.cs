using System;
using System.Collections.Generic;

// Root object serialized to/from JSON. Kept flat and minimal by design —
// this project does not persist world state (enemy positions, destroyed
// objects, player coordinates), only progression-level data.
[Serializable]
public class SaveData
{
    public List<LevelProgress> levels = new List<LevelProgress>();
    public PlayerStats playerStats = new PlayerStats();
    public List<string> inventory = new List<string>();
    public GameSettings settings = new GameSettings();
}

[Serializable]
public class LevelProgress
{
    public int levelIndex;
    public bool isCompleted;
    public bool isUnlocked;
}

[Serializable]
public class PlayerStats
{
    public int totalEnemiesDefeated;
    public int totalDeaths;
}

[Serializable]
public class GameSettings
{
    public float musicVolume = 1f;
    public float sfxVolume = 1f;
}