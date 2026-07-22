using UnityEngine;

public class ZombieBrute : EnemyBase, IAttackable
{
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private float engageRange = 2f; // must be closer than normal detection to provoke Brute

    protected override void Awake()
    {
        base.Awake();
        maxHealth = 100;      // tankiest
        currentHealth = maxHealth;
        moveSpeed = 1.2f;     // slowest
    }

    protected override void Start()
    {
        base.Start();
        ChangeState(new DefensiveState());
    }

    protected override void Update()
    {
        // Brute has custom logic: only starts chasing if player enters its short engage range,
        // otherwise it just stands guard — this is its "unique behaviour"
        if (currentState is DefensiveState && playerTarget != null)
        {
            float distance = Vector2.Distance(transform.position, playerTarget.position);
            if (distance <= engageRange)
            {
                ChangeState(new ChaseState());
            }
        }
        else
        {
            base.Update();
        }
    }

    public void PerformAttack(GameObject target)
    {
        Debug.Log(gameObject.name + " slams " + target.name);
        IDamageable damageable = target.GetComponent<IDamageable>();
        damageable?.TakeDamage(attackDamage);
    }
}