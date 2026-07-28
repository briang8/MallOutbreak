using UnityEngine;

// Tracks completion conditions for the current level: all enemies defeated
// and all collectibles collected. Once both are satisfied, unlocks the
// level's exit and marks the level complete.
public class LevelObjectiveTracker : MonoBehaviour
{
    [SerializeField] private Door exitDoor;

    private int _totalEnemies;
    private int _totalCollectibles;
    private int _enemiesDefeated;
    private int _collectiblesCollected;

    private void OnEnable()
    {
        EnemyBase.OnEnemyDefeated += OnEnemyDefeated;
        PlayerInventory.OnItemAdded += OnItemCollected;
    }

    private void OnDisable()
    {
        EnemyBase.OnEnemyDefeated -= OnEnemyDefeated;
        PlayerInventory.OnItemAdded -= OnItemCollected;
    }

    private void Start()
    {
        _totalEnemies = FindObjectsByType<EnemyBase>(FindObjectsSortMode.None).Length;
        _totalCollectibles = FindObjectsByType<Collectible>(FindObjectsSortMode.None).Length;

        if (exitDoor != null)
        {
            exitDoor.SetLocked(true); // exit starts locked until objectives are met
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

    private void CheckObjectives()
    {
        Debug.Log("Progress: " + _enemiesDefeated + "/" + _totalEnemies + " enemies, " +
                   _collectiblesCollected + "/" + _totalCollectibles + " collectibles");

        if (_enemiesDefeated >= _totalEnemies && _collectiblesCollected >= _totalCollectibles)
        {
            if (exitDoor != null) exitDoor.SetLocked(false);
            Debug.Log("Objectives complete — exit unlocked");
        }
    }
}