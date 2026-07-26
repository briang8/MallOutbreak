using NUnit.Framework;
using UnityEngine;

public class DamageTests
{

    [Test]
    public void TakeDamage_ReducesHealthCorrectly()
    {
        GameObject obj = new GameObject();
        PlayerHealth health = obj.AddComponent<PlayerHealth>();
        health.InitializeForTesting();
        
        health.TakeDamage(30);
        
        Assert.AreEqual(70, health.GetCurrentHealthForTesting());
    }

    [Test]
    public void TakeDamage_CannotGoBelowZero()
    {
        GameObject obj = new GameObject();
        PlayerHealth health = obj.AddComponent<PlayerHealth>();
        health.InitializeForTesting();

    health.TakeDamage(999);

    Assert.AreEqual(0, health.GetCurrentHealthForTesting());
    }
}