using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Item")]
public class ItemData : ObjectData
{

    public string itemName;
    
    [TextArea(3, 10)]public string itemDesc;




    void OnValidate()
    {
        itemName = name;
    }
}


