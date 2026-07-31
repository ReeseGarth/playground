using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private readonly HashSet<string> items = new();

    public event Action<string> ItemAdded;

    public bool Add(string itemId)
    {
        if (!items.Add(itemId))
        {
            return false;
        }

        Debug.Log($"Added {itemId} to inventory");

        ItemAdded?.Invoke(itemId);

        return true;
    }

    public bool Contains(string itemId)
    {
        return items.Contains(itemId);
    }
}
