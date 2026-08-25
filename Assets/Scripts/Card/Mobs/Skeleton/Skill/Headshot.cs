using UnityEngine;

public class Headshot : MobSkill
{
    public Headshot(BoardMob owner)
    {
        this.owner = owner;
        foreach (var data in owner.skillData)
        {
            if (data.skillName == "Headshot")
            {
                this.data = data;
                break;
            }
        }
        skillName = data.skillName;
        skillDesc = data.skillDesc;
        cost = data.cost;
        cooldown = data.cooldown;
        Skeleton skeleton = owner as Skeleton;
        skeleton.critValue = 3;
    }

    public override float ModifyValue(ModifyType type, float value = 0, float additonalValue = 0)
    {
        if (type == ModifyType.DamageDealt)
        {
            int dice = Random.Range(1, 10);
            if ((owner as Skeleton).validRolls.Contains(dice))
            {
                value *= data.value+additonalValue;
            }
        }
        return value;
    }

}
