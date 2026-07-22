using UnityEngine;

public interface IEnemyState
{
    void Enter(EnemyBase enemy);
    void Update(EnemyBase enemy);
    void Exit(EnemyBase enemy);
}
