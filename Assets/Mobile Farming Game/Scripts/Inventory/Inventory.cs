using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    [SerializeField] private List<InventoryItem> _items = new List<InventoryItem>();

    public void CropHarvestedCallback(CropType cropType)
    {
        bool cropFound = false;
        Debug.Log(cropType + "has been harvested");
        for (int i = 0; i < _items.Count; i++)
        {
            InventoryItem item = _items[i];
            if (item.cropType == cropType)
            {
                item.amount++;
                cropFound = true;
                break;
            }
        }
        //DebugInventory();

        if (cropFound)
            return;
        _items.Add(new InventoryItem(cropType, 1));

    }
    public InventoryItem[] GetInventoryItems()
    {
        return _items.ToArray();
    }
    public void Clear()
    {
        _items.Clear();
        
    }
    public void DebugInventory()
    {
        foreach(InventoryItem item in _items)
        {
            Debug.Log("We have" + item.amount + "items in our " + item.cropType + "list.");
        }
    }
}
