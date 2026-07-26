using UnityEngine;

// Central listener for game-over/level-complete/pause panel visibility.
// Each panel is a plain UI GameObject toggled on/off — no separate scene
// loads for these, since they're overlays on top of active gameplay.
public class GameStateUI : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;

    private bool _isPaused;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDied += ShowGameOver;
        LevelManager.OnLevelCompleted += ShowLevelComplete;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDied -= ShowGameOver;
        LevelManager.OnLevelCompleted -= ShowLevelComplete;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        _isPaused = !_isPaused;
        pausePanel.SetActive(_isPaused);
        Time.timeScale = _isPaused ? 0f : 1f;
    }

    private void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowLevelComplete()
    {
        levelCompletePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnRestartClicked()
    {
        Time.timeScale = 1f;
        LevelManager.Instance.RestartCurrentLevel();
    }

    public void OnResumeClicked()
    {
        TogglePause();
    }

    public void OnMainMenuClicked()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}