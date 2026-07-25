using UnityEngine;
using UnityEngine.UI;

// Represents a single level's button on the Level Selection screen.
// Reads its own state from SaveManager on screen load rather than the
// screen polling every level's status itself — keeps this self-contained
// and reusable if the number of levels ever changes.
public class LevelButton : MonoBehaviour
{
    [SerializeField] private int levelIndex;
    [SerializeField] private Image lockIcon;
    [SerializeField] private Image completedIcon;
    [SerializeField] private Button button;

    private void Start()
    {
        Refresh();
    }

    private void Refresh()
    {
        LevelProgress progress = SaveManager.Instance.CurrentSave.levels.Find(l => l.levelIndex == levelIndex);
        if (progress == null) return;

        lockIcon.gameObject.SetActive(!progress.isUnlocked);
        completedIcon.gameObject.SetActive(progress.isCompleted);
        button.interactable = progress.isUnlocked;
    }

    public void OnClicked()
    {
        LevelManager.Instance.LoadLevel(levelIndex);
    }
}