using UnityEngine;

public class OverchargedShot : MobSkill
{
    public int critValue;
    
    public OverchargedShot(BoardMob owner)
    {
        this.owner = owner;

        foreach (var data in owner.skillData)
        {
            if (data.skillName == "Overcharged Shot")
            {
                this.data = data;
                break;
            }
        }
        skillName = data.skillName;
        skillDesc = data.skillDesc;
        cost = data.cost;
        ultCost = data.ultCost;
        cooldown = data.cooldown;
        //? result: Cost: x Star | Ult: x Ult Star
        costDesc = "Cost: " + GameUtils.NBSP + cost + GameUtils.NBSP + " Star" + " | " +
                    "Ult: " + GameUtils.NBSP + ultCost + GameUtils.NBSP + " Ult Star";


    }
    public override void ApplyEffect(BoardMob target)
    {
        if (owner.validTarget.Contains(target))
        {
            if (owner.owner.star >= cost && owner.canUlt && owner.owner.ultStar >= ultCost && owner.heldItem is SkeletonItem)
            {
                owner.owner.ChangeStar(-cost);
                owner.owner.ChangeUltStar(-ultCost);
                
                float dmg = owner.finalAtk * 7.5f;

                dmg = GameUtils.CalculateMobDamage(dmg, owner, target, true);
                target.ChangeHealth(-dmg);
                owner.owner.isTargeting = false;
                owner.owner.RefreshButton();
                owner.canUlt = false;
            }
        }
    }


    public override void OnTurnFinish()
    {
        if (duration > 0)
        {
            duration--;
        }
    }
}
