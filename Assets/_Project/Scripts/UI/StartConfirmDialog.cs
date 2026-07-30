using UnityEngine;
// Shown only when the player has existing progress and clicks Start,
// to prevent accidentally overwriting a completed run.
public class StartConfirmDialog : MonoBehaviour
{
    public void OnConfirmYes()
    {
        SaveManager.Instance.ResetProgress();
        LevelManager.Instance.LoadLevel(1);
    }

    public void OnConfirmNo()
    {
        gameObject.SetActive(false);
    }
}