using UnityEngine;

public class ZombieRunner : EnemyBase, IAttackable
{
    [SerializeField] private int attackDamage = 6;

    protected override void Awake()
    {
        base.Awake();
        maxHealth = 25;       // lower health than Walker
        currentHealth = maxHealth;
        moveSpeed = 4.5f;     // faster than Walker
    }

    protected override void Start()
    {
        base.Start();
        ChangeState(new IdleState());
    }

    public void PerformAttack(GameObject target)
    {
        Debug.Log(gameObject.name + " lunges at " + target.name);
        IDamageable damageable = target.GetComponent<IDamageable>();
        damageable?.TakeDamage(attackDamage);
    }
}