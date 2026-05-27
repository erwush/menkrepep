using UnityEngine;

public class ChargedShot : MobSkill
{
    public int critValue;
    
    public ChargedShot(BoardMob owner)
    {
        this.owner = owner;

        foreach (var data in owner.skillData)
        {
            if (data.skillName == "Charged Shot")
            {
                this.data = data;
                break;
            }
        }
        skillName = data.skillName;
        skillDesc = data.skillDesc;
        cost = data.cost;
        cooldown = data.cooldown;
        critValue = (owner as Skeleton).critValue + 1;
        //? result: Cost: x Star
        costDesc = "Cost: " + GameUtils.NBSP + cost + GameUtils.NBSP + " Star";


    }
    public override void ApplyEffect(BoardMob target)
    {
        if (owner.validTarget.Contains(target))
        {
            if (owner.owner.star >= cost)
            {
                owner.owner.ChangeStar(-cost);
                float dmg = owner.finalAtk * 1.75f;
                dmg = GameUtils.CalculateMobDamage(dmg, owner, target, false, 1);
                target.ChangeHealth(-dmg);
                owner.owner.isTargeting = false;
                owner.owner.RefreshButton();
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
