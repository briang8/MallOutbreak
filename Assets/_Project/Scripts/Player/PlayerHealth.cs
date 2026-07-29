using UnityEngine;

using System;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    private int _currentHealth;

    public static event Action<int, int> OnHealthChanged; // (current, max)
    public static event Action OnPlayerDied;

    public int GetCurrentHealthForTesting() => _currentHealth;

    // Exposes initialization for unit tests
    public void InitializeForTesting()
    {
        _currentHealth = maxHealth;
    }

    private void Start()
    {
        OnHealthChanged?.Invoke(_currentHealth, maxHealth);
    }

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (_currentHealth <= 0) return;

        _currentHealth -= amount;
         _currentHealth = Mathf.Max(_currentHealth, 0);

    if (AudioManager.Instance != null) AudioManager.Instance.PlayPlayerHit();
    OnHealthChanged?.Invoke(_currentHealth, maxHealth);

    if (_currentHealth <= 0)
    {
        Die();
    }
    }

    private void Die()
    {
    OnPlayerDied?.Invoke();
    if (SaveManager.Instance != null)
    {
        SaveManager.Instance.CurrentSave.playerStats.totalDeaths++;
        SaveManager.Instance.Save();
    }
    }

    public void Heal(int amount)
         {
            _currentHealth = Mathf.Min(_currentHealth + amount, maxHealth);
            OnHealthChanged?.Invoke(_currentHealth, maxHealth);
         }

}