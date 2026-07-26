using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SearchAlgorithmTests
{
    [Test]
    public void FindNearest_ReturnsClosestEnemy()
    {
        GameObject farObj = new GameObject();
        farObj.transform.position = new Vector3(10, 0, 0);
        EnemyBase far = farObj.AddComponent<ZombieWalker>();

        GameObject nearObj = new GameObject();
        nearObj.transform.position = new Vector3(1, 0, 0);
        EnemyBase near = nearObj.AddComponent<ZombieWalker>();

        List<EnemyBase> enemies = new List<EnemyBase> { far, near };

        EnemyBase result = NearestEnemyFinder.FindNearest(Vector2.zero, enemies);

        Assert.AreEqual(near, result);
    }

    [Test]
    public void FindNearest_ReturnsNullForEmptyList()
    {
        List<EnemyBase> enemies = new List<EnemyBase>();

        EnemyBase result = NearestEnemyFinder.FindNearest(Vector2.zero, enemies);

        Assert.IsNull(result);
    }

    [TearDown]
    
    public void Cleanup()
    {
        EnemyBase.ActiveEnemies.Clear();
    }
}