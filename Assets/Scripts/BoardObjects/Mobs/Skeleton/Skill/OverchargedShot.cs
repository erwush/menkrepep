using UnityEngine;

public class OverchargedShot : MobSkill
{
    public int critValue;
    
    public OverchargedShot(BoardMob owner)
    {
        this.owner = owner;

        data = owner.skillData[2];
        skillName = data.skillName;
        skillDesc = data.skillDesc;
        cost = data.cost;
        ultCost = data.ultCost;
        cooldown = data.cooldown;


    }
    public override void ApplyEffect(BoardMob target)
    {
        if (owner.validTarget.Contains(target))
        {
            if (owner.owner.star >= cost && owner.canUlt && owner.owner.ultStar >= ultCost && owner.heldItem is SkeletonItem)
            {
                owner.owner.ChangeStar(-cost);
                owner.owner.ChangeUltStar(-ultCost);
                int dice = Random.Range(1, 10);
                float dmg = owner.finalAtk;
                if ((owner as Skeleton ).validRolls.Contains(dice))
                {
                    dmg *= critValue;
                }
                dmg = GameUtils.CalculateMobDamage(owner, target, true);
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
