using NUnit.Framework;
using UnityEngine;

public class InventoryTests
{
    [Test]
    public void AddItem_AddsToInventory()
    {
        GameObject obj = new GameObject();
        PlayerInventory inventory = obj.AddComponent<PlayerInventory>();

        inventory.AddItem("GoldKey");

        Assert.IsTrue(inventory.HasItem("GoldKey"));
    }

    [Test]
    public void HasItem_ReturnsFalseForMissingItem()
    {
        GameObject obj = new GameObject();
        PlayerInventory inventory = obj.AddComponent<PlayerInventory>();

        Assert.IsFalse(inventory.HasItem("NonExistentItem"));
    }
}