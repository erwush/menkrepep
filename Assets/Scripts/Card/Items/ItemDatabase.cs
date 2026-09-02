using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

[CreateAssetMenu(menuName = "Database/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] private List<ItemData> allItems;
    
    private Dictionary<string, ItemData> _lookup;
    
    private void OnEnable()
    {
        _lookup = allItems.ToDictionary(i => i.itemID, i => i);
    }
    
    public ItemData GetByID(string id)
    {
        if (_lookup.TryGetValue(id, out var item)) return item;
        Debug.LogWarning($"Item ID '{id}' not found!");
        return null;
    }
}