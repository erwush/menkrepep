using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData", menuName = "Scriptable Objects/ObjectData")]
public abstract class ObjectData : ScriptableObject
{
    public string objName, objectID; 
    public Sprite icon;
    public int cost;
    public UnitType type;
}
