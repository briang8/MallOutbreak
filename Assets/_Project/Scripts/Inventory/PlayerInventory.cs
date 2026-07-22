using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerInventory : MonoBehaviour
{
    private List<string> _items = new List<string>();

    public static event Action<string> OnItemAdded;

    public void AddItem(string itemId)
    {
        _items.Add(itemId);
        Debug.Log("Inventory now contains: " + string.Join(", ", _items));
        OnItemAdded?.Invoke(itemId);
    }

    public bool HasItem(string itemId)
    {
        return _items.Contains(itemId);
    }
}