using UnityEngine;

public class Phantasm : MobSkill
{

    public Phantasm(BoardMob owner)
    {
        this.owner = owner;
        data = owner.skillData[2];
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
        if (owner.owner.ultStar >= 20 && owner.canUlt && owner.owner.star >= cost && owner.heldItem is PhantomItem)
        {
            owner.owner.ChangeStar(-data.cost);
            owner.owner.ChangeUltStar(-20);
            owner.cdReduction += 1;
            Phantom phantom = owner as Phantom;
            phantom.isPhantasm = true;
            foreach (var skill in phantom.skills)
            {
                if (skill is Haunt haunt) haunt.hauntDur += 2;
            }
            duration = 20;
            owner.canUlt = false;
            owner.owner.isTargeting = false;
            owner.owner.RefreshButton();
        }
    }


    public override void OnTurnFinish()
    {
      
        if (duration > 0 && (owner as Phantom).isPhantasm)
        {
            duration--;
              Debug.Log("duration: " + duration);
            if (duration <= 0) RemoveEffect(owner);
        }

    }

    public override void RemoveEffect(BoardMob target)
    {
        Phantom phantom = owner as Phantom;
        phantom.cdReduction -= 1;
        phantom.isPhantasm = false;
        foreach (var skill in phantom.skills)
        {
            if (skill is Haunt haunt) haunt.hauntDur -= 2;
        }
    }
}
