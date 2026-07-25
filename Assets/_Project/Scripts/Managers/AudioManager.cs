using UnityEngine;

// Central audio playback point. Gameplay/UI systems never call AudioSource
// directly — they either fire an event this manager listens for, or call
// a public method here. Keeps all mixing/volume logic in one place.
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Clips")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip playerHitClip;
    [SerializeField] private AudioClip enemyHitClip;
    [SerializeField] private AudioClip enemyDeathClip;
    [SerializeField] private AudioClip itemPickupClip;
    [SerializeField] private AudioClip uiClickClip;
    [SerializeField] private AudioClip doorOpenClip;

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

    private void Start()
    {
        PlayMusic();
    }

    private void OnEnable()
    {
        EnemyBase.OnEnemyDefeated += HandleEnemyDefeated;
        PlayerInventory.OnItemAdded += HandleItemAdded;
    }

    private void OnDisable()
    {
        EnemyBase.OnEnemyDefeated -= HandleEnemyDefeated;
        PlayerInventory.OnItemAdded -= HandleItemAdded;
    }

    private void HandleEnemyDefeated(EnemyBase enemy) => PlaySfx(enemyDeathClip);
    private void HandleItemAdded(string item) => PlaySfx(itemPickupClip);

    public void PlayMusic()
    {
        if (backgroundMusic == null || musicSource == null) return;
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    private void PlaySfx(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayPlayerHit() => PlaySfx(playerHitClip);
    public void PlayEnemyHit() => PlaySfx(enemyHitClip);
    public void PlayUiClick() => PlaySfx(uiClickClip);
    public void PlayDoorOpen() => PlaySfx(doorOpenClip);

    public void SetMusicVolume(float value)
    {
        if (musicSource != null) musicSource.volume = value;
    }

    public void SetSfxVolume(float value)
    {
        if (sfxSource != null) sfxSource.volume = value;
    }
}