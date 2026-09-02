using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Item")]
public class ItemData : ObjectData
{

    public Description decs;
    public  string itemID => objectID;
    
    




    void OnValidate()
    {
        objectID = name;
        type = UnitType.Item;
    }
}


