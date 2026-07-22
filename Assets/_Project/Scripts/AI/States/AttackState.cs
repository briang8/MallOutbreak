using UnityEngine;

public class AttackState : IEnemyState
{
    private float _cooldown = 1f;
    private float _timer;

    public void Enter(EnemyBase enemy)
    {
        _timer = 0f;
    }

    public void Update(EnemyBase enemy)
    {
        if (enemy.PlayerTarget == null) return;

        float distance = Vector2.Distance(enemy.transform.position, enemy.PlayerTarget.position);
        if (distance > enemy.AttackRange)
        {
            enemy.ChangeState(new ChaseState());
            return;
        }

        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            IAttackable attacker = enemy as IAttackable;
            attacker?.PerformAttack(enemy.PlayerTarget.gameObject);
            _timer = _cooldown;
        }
    }

    public void Exit(EnemyBase enemy) { }
}