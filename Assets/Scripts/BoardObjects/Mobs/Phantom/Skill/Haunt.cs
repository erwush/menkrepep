using UnityEngine;

public class Haunt : MobSkill
{

    public int hauntDur;
    public Haunt(BoardMob owner)
    {
        this.owner = owner;

        data = owner.skillData[1];
        skillName = data.skillName;
        skillDesc = data.skillDesc;
        cost = data.cost;
        cooldown = data.cooldown;
        hauntDur = 4;


    }
    public override void ApplyEffect(BoardMob target)
    {
        if (owner.validTarget.Contains(target))
        {
            if (duration <= 0 && owner.owner.star >= cost)
            {
                float demeg = GameUtils.CalculateMobDamage(owner, target);
                target.ChangeHealth(-demeg);
                owner.owner.ChangeStar(-cost);
                target.ApplyEffect(new Haunted(hauntDur, target), owner);
                duration = cooldown - owner.cdReduction;


            }
            else
            {
                return;
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
