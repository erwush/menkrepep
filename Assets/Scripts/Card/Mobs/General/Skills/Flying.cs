using UnityEngine;

public class Flying : MobSkill
{
    
    public Flying(BoardMob owner)
    {
        this.owner = owner;
        foreach(var data in owner.skillData)
        {
            if (data.skillName == "Flying")
            {
                this.data = data;
                break;
            }
        }
        skillName = data.skillName;
        skillDesc = data.desc.description;
        owner.bonusSpd = data.value;
        
    }
}
