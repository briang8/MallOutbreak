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
        Vector2 nextPosition = (Vector2)enemy.transform.position + direction * enemy.MoveSpeed * Time.deltaTime;

        if (!enemy.IsBlocked(nextPosition))
        {
            enemy.transform.position = nextPosition;
        }

        enemy.FaceDirection(direction.x);

        Animator anim = enemy.GetComponent<Animator>();
        if (anim != null) anim.SetFloat("Speed", enemy.MoveSpeed);
    }

    public void Exit(EnemyBase enemy) { }
}