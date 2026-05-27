using UnityEngine;

public class MembraneSkin : MobSkill
{


    public MembraneSkin(BoardMob owner)
    {

        this.owner = owner;
        foreach (var data in owner.skillData)
        {
            if (data.skillName == "Membrane Skin")
            {
                this.data = data;
                break;
            }
        }
        skillName = data.skillName;
        skillDesc = data.skillDesc;
        cost = data.cost;
        cooldown = data.cooldown;
    }

    public override float ModifyValue(ModifyType type, float value = 0, float additonalValue = 0)
    {
        if(type == ModifyType.DamageTaken)
        {
            
        Phantom phantom = owner as Phantom;
        
          if (Mathf.Abs(value) > phantom.maxHp)
            {
                //?0.25 means it is 75% damage reduction so * 0.25
                //?ternary operator, if phantasm, 0.75, if not then 0.5
                Mathf.Ceil(value *= phantom.isPhantasm ? 0.25f : 0.5f);
                if (!phantom.isPhantasm)
                {
                    Weakness weakness = new Weakness(3, 4);
                    if (!phantom.statusEffects.Contains(weakness)) owner.ApplyEffect(weakness, this.owner);
                    else weakness.ResetEffect();
                }
            }
        }
        return (int)value;
    }

   
    

}
