using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData", menuName = "Scriptable Objects/ObjectData")]
public abstract class ObjectData : ScriptableObject
{
    public int cost;
}
