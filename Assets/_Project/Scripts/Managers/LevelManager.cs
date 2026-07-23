using UnityEngine;
using UnityEngine.SceneManagement;

// Central point for all level-loading decisions. UI and gameplay code should
// call through here rather than calling SceneManager directly, so unlock
// checks and future transition effects (fade, loading screen) live in one place.
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public int CurrentLevelIndex { get; private set; }

    private readonly string[] _levelSceneNames =
    {
        "", // index 0 unused, levels are 1-indexed to match design docs
        "Level1_Supermarket",
        "Level2_FoodCourt",
        "Level3_ClothingStore",
        "Level4_Electronics",
        "Level5_ParkingRoof"
    };

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

    public void LoadLevel(int levelIndex)
    {
        LevelProgress progress = SaveManager.Instance.CurrentSave.levels.Find(l => l.levelIndex == levelIndex);

        if (progress == null || !progress.isUnlocked)
        {
            Debug.Log("Level " + levelIndex + " is locked, cannot load");
            return;
        }

        CurrentLevelIndex = levelIndex;
        SceneManager.LoadScene(_levelSceneNames[levelIndex]);
    }

    public void CompleteCurrentLevel()
    {
        SaveManager.Instance.MarkLevelCompleted(CurrentLevelIndex);
        Debug.Log("Level " + CurrentLevelIndex + " marked completed");
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(_levelSceneNames[CurrentLevelIndex]);
    }

    private void Update()
    {
        
    }
}