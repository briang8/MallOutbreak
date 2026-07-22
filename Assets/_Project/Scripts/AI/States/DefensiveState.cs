using UnityEngine;

public class DefensiveState : IEnemyState
{
    public void Enter(EnemyBase enemy)
    {
        Debug.Log(enemy.name + " is guarding");
    }

    public void Update(EnemyBase enemy)
    {
        // Brute holds position, only reacts to close threats — handled in ZombieBrute override
    }

    public void Exit(EnemyBase enemy) { }
}