using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void OnStartGameClicked()
    {
        LevelManager.Instance.LoadLevel(1);
    }
}