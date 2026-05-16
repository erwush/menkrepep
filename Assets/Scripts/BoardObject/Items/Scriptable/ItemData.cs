using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public string itemName, itemDesc;
    public Sprite icon;
    public int cost;


    void OnValidate()
    {
        itemName = name;
    }
}


