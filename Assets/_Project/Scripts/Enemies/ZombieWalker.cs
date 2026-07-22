using UnityEngine;

public class ZombieWalker : EnemyBase, IAttackable
{
    [SerializeField] private int attackDamage = 10;

    protected override void Start()
    {
        base.Start();
        ChangeState(new IdleState());
    }

    public void PerformAttack(GameObject target)
    {
        Debug.Log(gameObject.name + " attacks " + target.name);
        IDamageable damageable = target.GetComponent<IDamageable>();
        damageable?.TakeDamage(attackDamage);
    }
}