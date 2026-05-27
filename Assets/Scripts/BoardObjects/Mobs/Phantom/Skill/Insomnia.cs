using UnityEngine;

public class Insomnia : MobSkill
{
    public int costCounter;
    public bool doNothing;

    public Insomnia(BoardMob owner)
    {
        this.owner = owner;

        foreach (var data in owner.skillData)
        {
            if (data.skillName == "Insomnia")
            {
                this.data = data;
                break;
            }
        }
        skillName = data.skillName;
        skillDesc = data.skillDesc;
        cost = data.cost;
        cooldown = data.cooldown;
        costCounter = -1;
        doNothing = false;



    }

    public override void OnTurnStart()
    {
        if (doNothing)
        {
            owner.owner.ChangeStar(1);
            if ((owner as Phantom).isPhantasm) owner.owner.ChangeUltStar(1);
        }
        costCounter = owner.owner.star;
        base.OnTurnStart();
    }

    public override void OnTurnEnd()
    {
         if (costCounter == owner.owner.star) doNothing = true;
        else doNothing = false;
        base.OnTurnEnd();
    }

}
