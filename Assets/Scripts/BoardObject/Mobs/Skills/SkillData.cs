using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Skill")]
public class SkillData : ScriptableObject
{

    public string skillName, skillDesc;
    public int cost, cooldown;

    

}
