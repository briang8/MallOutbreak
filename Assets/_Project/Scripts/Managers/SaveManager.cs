using UnityEngine;
using System.IO;
using Newtonsoft.Json;

// Singleton responsible for all save/load I/O. Nothing outside this class
// should touch the save file directly — other systems read/write through
// the in-memory CurrentSave reference and call Save()/Load() explicitly.
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    public SaveData CurrentSave { get; private set; }

    private string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }

    public void Save()
    {
        string json = JsonConvert.SerializeObject(CurrentSave, Formatting.Indented);
        File.WriteAllText(SavePath, json);
        Debug.Log("Game saved to: " + SavePath);
    }

    public void Load()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            CurrentSave = JsonConvert.DeserializeObject<SaveData>(json);
            Debug.Log("Save loaded from: " + SavePath);
        }
        else
        {
            CurrentSave = CreateNewSave();
            Debug.Log("No save file found, created new save data");
        }
    }

    // Builds a fresh save with level 1 unlocked and everything else locked.
    // Called on first-ever launch or if the save file is missing/corrupted.
    private SaveData CreateNewSave()
    {
        SaveData data = new SaveData();
        for (int i = 1; i <= 5; i++)
        {
            data.levels.Add(new LevelProgress
            {
                levelIndex = i,
                isCompleted = false,
                isUnlocked = (i == 1) // only level 1 starts unlocked
            });
        }
        return data;
    }

    public void MarkLevelCompleted(int levelIndex)
    {
        LevelProgress progress = CurrentSave.levels.Find(l => l.levelIndex == levelIndex);
        if (progress == null) return;

        progress.isCompleted = true;

        // unlock the next level, if one exists
        LevelProgress nextLevel = CurrentSave.levels.Find(l => l.levelIndex == levelIndex + 1);
        if (nextLevel != null)
        {
            nextLevel.isUnlocked = true;
        }

        Save();
    }

    public bool HasAnyProgress()
    {
        return CurrentSave.levels.Exists(l => l.isCompleted);
    }
}