using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject startConfirmPanel;

    private void Start()
    {
        // Continue Game only makes sense if there's existing progress;
        // hiding it otherwise avoids a dead button confusing the player
        if (continueButton != null)
        {
            continueButton.SetActive(SaveManager.Instance.HasAnyProgress());
        }
    }

    public void OnStartGameClicked()
    {
        if (SaveManager.Instance.HasAnyProgress())
        {
            startConfirmPanel.SetActive(true);
        }
        else
        {
            LevelManager.Instance.LoadLevel(1);
        }
    }

    public void OnContinueGameClicked()
    {
        int lastUnlocked = 1;
        foreach (var level in SaveManager.Instance.CurrentSave.levels)
        {
            if (level.isUnlocked) lastUnlocked = level.levelIndex;
        }
        LevelManager.Instance.LoadLevel(lastUnlocked);
    }

    public void OnLevelSelectionClicked()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    [SerializeField] private GameObject settingsPanel;
    public void OnSettingsClicked()
    {
        settingsPanel.SetActive(true);
    }

    public void OnExitClicked()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
    Application.Quit();
    #endif
    }

}