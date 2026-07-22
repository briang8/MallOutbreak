using UnityEngine;
using System;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected int maxHealth = 50;
    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected float detectionRange = 5f;
    [SerializeField] protected float attackRange = 1f;

    protected int currentHealth;
    protected IEnemyState currentState;
    protected Transform playerTarget;

    public static event Action<EnemyBase> OnEnemyDefeated;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
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

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " defeated");
        OnEnemyDefeated?.Invoke(this);
        gameObject.SetActive(false); // pooling-friendly: deactivate instead of Destroy, real pooling logic in Phase 13
    }

    // exposed so states can read/move this enemy without needing public setters on private fields
    public Transform PlayerTarget => playerTarget;
    public float MoveSpeed => moveSpeed;
    public float DetectionRange => detectionRange;
    public float AttackRange => attackRange;
}