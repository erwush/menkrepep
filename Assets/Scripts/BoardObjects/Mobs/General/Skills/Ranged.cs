using UnityEngine;

public class Ranged : MobSkill
{
    
      public Ranged(BoardMob owner)
    {
        this.owner = owner;
        foreach(var data in owner.skillData)
        {
            if (data.skillName == "Ranged")
            {
                this.data = data;
                break;
            }
        }
        skillName = data.skillName;
        skillDesc = data.skillDesc;
        owner.atkRange = data.value;
        
    }
}
