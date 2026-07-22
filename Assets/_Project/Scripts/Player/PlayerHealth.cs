using UnityEngine;

using System;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    private int _currentHealth;

    public static event Action<int, int> OnHealthChanged; // (current, max)
    public static event Action OnPlayerDied;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (_currentHealth <= 0) return; // already dead, ignore further damage

        _currentHealth -= amount;
        _currentHealth = Mathf.Max(_currentHealth, 0);

        OnHealthChanged?.Invoke(_currentHealth, maxHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
         {
            _currentHealth = Mathf.Min(_currentHealth + amount, maxHealth);
            OnHealthChanged?.Invoke(_currentHealth, maxHealth);
         }


    private void Die()
    {
        OnPlayerDied?.Invoke();
        Debug.Log("Player has died! Game Over.");
    }
}