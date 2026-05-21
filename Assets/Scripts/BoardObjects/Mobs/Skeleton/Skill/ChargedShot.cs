using UnityEngine;

public class ChargedShot : MobSkill
{
    public int critValue;
    public ChargedShot(BoardMob owner)
    {
        this.owner = owner;

        data = owner.skillData[1];
        skillName = data.skillName;
        skillDesc = data.skillDesc;
        cost = data.cost;
        cooldown = data.cooldown;



    }
    public override void ApplyEffect(BoardMob target)
    {
        if (owner.validTarget.Contains(target))
        {

            if (owner.owner.star >= cost)
            {
                owner.owner.ChangeStar(-cost);
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
