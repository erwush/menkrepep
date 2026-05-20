using UnityEngine;

public class SupersonicFlight : MobSkill
{


    public SupersonicFlight(BoardMob owner)
    {

        this.owner = owner;
        data = owner.skillData[1];
        skillName = data.skillName;
        skillDesc = data.skillDesc;
        cost = data.cost;
        cooldown = data.cooldown;
    }

    public override void OnSelected()
    {
        owner.owner.isTargeting = true;
        owner.owner.RefreshButton();
    }

    public override void OnUnselected()
    {
        owner.owner.isTargeting = false;
        owner.owner.RefreshButton();
    }

    public override void ApplyEffect(BoardMob target)
    {
        if (duration <= 0 && owner.owner.star >= cost)
        {
            owner.owner.ChangeStar(-cost);
            owner.bonusSpd += 3;
            duration = cooldown - owner.cdReduction;
            used = true;
            owner.owner.isTargeting = false;
            owner.owner.RefreshButton();
        }
    }

    public override void RemoveEffect(BoardMob target)
    {
        owner.bonusSpd -= 3;
    }

    public override void OnTurnEnd()
    {
        if (used)
        {
            RemoveEffect(owner);
            used = false;
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
