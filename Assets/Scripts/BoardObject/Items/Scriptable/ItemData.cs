using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Item")]
public class ItemData : ObjectData
{
    public string itemName, itemDesc;




    void OnValidate()
    {
        itemName = name;
    }
}


