using UnityEngine;

// Tracks completion conditions for the current level. Requirements can be set
// in the Inspector, but fall back to per-level defaults when left at zero.
public class LevelObjectiveTracker : MonoBehaviour
{
    [SerializeField] private Door exitDoor;
    [SerializeField] private int requiredEnemies = 0;
    [SerializeField] private int requiredCollectibles = 0;
    [SerializeField] private int requiredChests = 0;

    private int _enemiesDefeated;
    private int _collectiblesCollected;
    private int _chestsOpened;

    private void OnEnable()
    {
        EnemyBase.OnEnemyDefeated += OnEnemyDefeated;
        PlayerInventory.OnItemAdded += OnItemCollected;
        Chest.OnChestOpened += OnChestOpened;
    }

    private void OnDisable()
    {
        EnemyBase.OnEnemyDefeated -= OnEnemyDefeated;
        PlayerInventory.OnItemAdded -= OnItemCollected;
        Chest.OnChestOpened -= OnChestOpened;
    }

    private void Start()
    {
        ApplyDefaultObjectivesIfNeeded();

        if (exitDoor != null)
        {
            exitDoor.SetLocked(true); // exit starts locked until objectives are met
        }

        CheckObjectives();
    }

    private void ApplyDefaultObjectivesIfNeeded()
    {
        if (requiredEnemies > 0 && requiredCollectibles > 0 && requiredChests > 0)
        {
            return;
        }

        int levelIndex = LevelManager.Instance != null ? LevelManager.Instance.CurrentLevelIndex : ResolveLevelIndexFromScene();

        switch (levelIndex)
        {
            case 1:
                requiredEnemies = 2;
                requiredCollectibles = 1;
                requiredChests = 1;
                break;
            case 2:
                requiredEnemies = 3;
                requiredCollectibles = 2;
                requiredChests = 1;
                break;
            case 3:
                requiredEnemies = 4;
                requiredCollectibles = 3;
                requiredChests = 1;
                break;
            case 4:
                requiredEnemies = 7;
                requiredCollectibles = 3;
                requiredChests = 2;
                break;
            case 5:
                requiredEnemies = 8;
                requiredCollectibles = 4;
                requiredChests = 4;
                break;
        }
    }

    private int ResolveLevelIndexFromScene()
    {
        switch (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
        {
            case "Level1_Supermarket":
                return 1;
            case "Level2_FoodCourt":
                return 2;
            case "Level3_ClothingStore":
                return 3;
            case "Level4_Electronics":
                return 4;
            case "Level5_ParkingRoof":
                return 5;
            default:
                return 0;
        }
    }

    private void OnEnemyDefeated(EnemyBase enemy)
    {
        _enemiesDefeated++;
        CheckObjectives();
    }

    private void OnItemCollected(string itemId)
    {
        _collectiblesCollected++;
        CheckObjectives();
    }

    private void OnChestOpened()
    {
        _chestsOpened++;
        CheckObjectives();
    }

    private void CheckObjectives()
    {
        Debug.Log("Progress: " + _enemiesDefeated + "/" + requiredEnemies + " enemies, " +
                   _collectiblesCollected + "/" + requiredCollectibles + " collectibles, chests opened: " +
                   _chestsOpened + "/" + requiredChests);

        bool enemiesComplete = _enemiesDefeated >= requiredEnemies;
        bool collectiblesComplete = _collectiblesCollected >= requiredCollectibles;
        bool chestComplete = _chestsOpened >= requiredChests;

        if (enemiesComplete && collectiblesComplete && chestComplete)
        {
            if (exitDoor != null) exitDoor.SetLocked(false);
            Debug.Log("Objectives complete — exit unlocked");
        }
    }
}