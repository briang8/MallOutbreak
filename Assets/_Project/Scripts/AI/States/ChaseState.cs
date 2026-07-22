using UnityEngine;

public class ChaseState : IEnemyState
{
    public void Enter(EnemyBase enemy) { }

    public void Update(EnemyBase enemy)
    {
        if (enemy.PlayerTarget == null) return;

        float distance = Vector2.Distance(enemy.transform.position, enemy.PlayerTarget.position);

        if (distance <= enemy.AttackRange)
        {
            enemy.ChangeState(new AttackState());
            return;
        }

        if (distance > enemy.DetectionRange)
        {
            enemy.ChangeState(new IdleState());
            return;
        }

        Vector2 direction = (enemy.PlayerTarget.position - enemy.transform.position).normalized;
        enemy.transform.position += (Vector3)direction * enemy.MoveSpeed * Time.deltaTime;
    }

    public void Exit(EnemyBase enemy) { }
}