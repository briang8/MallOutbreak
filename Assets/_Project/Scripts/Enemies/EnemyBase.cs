using UnityEngine;
using System;
using System.Collections.Generic;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected int maxHealth = 50;
    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected float detectionRange = 5f;
    [SerializeField] protected float attackRange = 1f;

    protected int currentHealth;
    protected IEnemyState currentState;
    protected Transform playerTarget;
    protected Animator animator;

    public static event Action<EnemyBase> OnEnemyDefeated;
    public static readonly List<EnemyBase> ActiveEnemies = new List<EnemyBase>();

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        ActiveEnemies.Add(this);
        animator = GetComponent<Animator>();
    }

    protected virtual void OnDisable()
    {
        ActiveEnemies.Remove(this);
    }

    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) playerTarget = playerObj.transform;
    }

    protected virtual void Update()
    {
        currentState?.Update(this);
    }

    public void ChangeState(IEnemyState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
        Debug.Log(gameObject.name + " entered state: " + newState.GetType().Name);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " took " + amount + " damage, health now: " + currentHealth);

        if (animator != null) animator.SetTrigger("HurtTrigger");

        if (currentHealth <= 0)
        {
            Die();
        }

        AudioManager.Instance.PlayEnemyHit();
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " defeated");
        if (animator != null) animator.SetBool("IsDead", true);
        OnEnemyDefeated?.Invoke(this);
        SaveManager.Instance.CurrentSave.playerStats.totalEnemiesDefeated++;
        Invoke(nameof(DeactivateAfterDeath), 1f);
    }

    private void DeactivateAfterDeath()
    {
        gameObject.SetActive(false);
    }

    public Transform PlayerTarget => playerTarget;
    public float MoveSpeed => moveSpeed;
    public float DetectionRange => detectionRange;
    public float AttackRange => attackRange;
}