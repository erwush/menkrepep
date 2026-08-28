using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Item")]
public class ItemData : ObjectData
{

    public string itemName;
    public Description decs;
    
    




    void OnValidate()
    {
        itemName = name;
    }
}


