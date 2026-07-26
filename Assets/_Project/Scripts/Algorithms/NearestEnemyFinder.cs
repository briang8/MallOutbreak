using UnityEngine;
using System.Collections.Generic;

// Linear search for the closest active enemy to a given position.

public static class NearestEnemyFinder
{
    public static EnemyBase FindNearest(Vector2 fromPosition, List<EnemyBase> enemies)
    {
        EnemyBase nearest = null;
        float nearestDistance = float.MaxValue;

        foreach (var enemy in enemies)
        {
            if (enemy == null || !enemy.gameObject.activeInHierarchy) continue;

            float distance = Vector2.Distance(fromPosition, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearest = enemy;
            }
        }

        return nearest;
    }
}