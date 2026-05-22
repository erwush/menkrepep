using UnityEngine;

public class NormalAttack : MobSkill
{

    public NormalAttack(BoardMob owner)
    {
        this.owner = owner;
        data = owner.skillData[0];
        skillName = data.skillName;
        skillDesc = data.skillDesc;
        cost = data.cost;
        cooldown = data.cooldown;
        //? result: Cost: 1 Star
        costDesc = "Cost: " + GameUtils.NBSP + cost + GameUtils.NBSP + " Star";
    }
    public override void ApplyEffect(BoardMob target)
    {
        owner.Attack(target);
    }

    public override void RefreshCost()
    {
        //? result: Cost: 1 Star
        costDesc = "Cost: " + GameUtils.NBSP + cost + GameUtils.NBSP + " Star";
    }

}
