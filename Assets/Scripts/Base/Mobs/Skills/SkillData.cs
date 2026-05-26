using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SkillData", menuName = "Skill")]
public class SkillData : ScriptableObject
{

    public string skillName; 
    [TextArea(3, 10)] public string skillDesc;
    public int cost, ultCost, cooldown;
    public int atkRange;
    public RangeType rangeType;
    public List<ExplainData> explanation = new List<ExplainData>();
    public Vector2Int[] atkDir = new Vector2Int[]{
        new (0, 1),   // atas
        new (0, -1),  // bawah
        new (1, 0),   // kanan
        new (-1, 0),  // kiri

        new (1, 1),   // kanan atas
        new (1, -1),  // kanan bawah
        new (-1, 1),  // kiri atas
        new (-1, -1), // kiri bawah
    };
}

public enum RangeType
{
    Single,
    Area,

}
