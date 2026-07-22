using UnityEngine;

public class IdleState : IEnemyState
{
    public void Enter(EnemyBase enemy) { }

    public void Update(EnemyBase enemy)
    {
        if (enemy.PlayerTarget == null) return;

        float distance = Vector2.Distance(enemy.transform.position, enemy.PlayerTarget.position);
        if (distance <= enemy.DetectionRange)
        {
            enemy.ChangeState(new ChaseState());
        }
    }

    public void Exit(EnemyBase enemy) { }
}