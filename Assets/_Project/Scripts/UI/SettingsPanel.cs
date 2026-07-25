using UnityEngine;
using UnityEngine.UI;

// Reusable settings panel — same GameObject/script can later be dropped
// into the in-game Pause panel too, since both just read/write the same
// SaveManager settings data.
public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void OnEnable()
    {
        musicSlider.value = SaveManager.Instance.CurrentSave.settings.musicVolume;
        sfxSlider.value = SaveManager.Instance.CurrentSave.settings.sfxVolume;
    }

    public void OnMusicChanged(float value)
    {
        SaveManager.Instance.CurrentSave.settings.musicVolume = value;
        AudioManager.Instance.SetMusicVolume(value);
    }

    public void OnSfxChanged(float value)
    {
        SaveManager.Instance.CurrentSave.settings.sfxVolume = value;
        AudioManager.Instance.SetSfxVolume(value);
    }

    public void OnCloseClicked()
    {
        SaveManager.Instance.Save();
        gameObject.SetActive(false);
    }
}