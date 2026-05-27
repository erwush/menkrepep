using UnityEngine;

public class Haunt : MobSkill
{

    public int hauntDur;
    public Haunt(BoardMob owner)
    {
        this.owner = owner;

        foreach(var data in owner.skillData)
        {
            if (data.skillName == "Haunt")
            {
                this.data = data;
                break;
            }
        }
        skillName = data.skillName;
        skillDesc = data.skillDesc;
        cost = data.cost;
        cooldown = data.cooldown;
        hauntDur = 4;
        //? result: Cost: x Star | Cooldown: x Turns
        costDesc = "Cost: " + GameUtils.NBSP + cost + GameUtils.NBSP + " Star" + " | " +
                    "Cooldown: " + GameUtils.NBSP + cooldown + GameUtils.NBSP + "Turns";


    }
    public override void ApplyEffect(BoardMob target)
    {
        if (owner.validTarget.Contains(target))
        {
            if (duration <= 0 && owner.owner.star >= cost)
            {
                float dmg = owner.finalAtk;
                dmg = GameUtils.CalculateMobDamage(dmg, owner, target);
                target.ChangeHealth(-dmg);
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
