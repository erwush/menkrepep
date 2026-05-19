using UnityEngine;

public class Haunt : MobSkill
{

    public int hauntDur;
      public Haunt(BoardMob owner)
    {
        this.owner = owner;
        
        data = owner.skillData[0];
        skillName = data.skillName;
        skillDesc = data.skillDesc;
        cost = data.cost;
        cooldown = data.cooldown;
        hauntDur = 4;
        
    }

    public override void ApplyEffect(BoardMob target)
    {
        if (duration <= 0)
        {
            target.ChangeHealth(GameUtils.CalculateMobDamage(owner, target));
            owner.owner.ChangeStar(-cost);
            target.statusEffects.Add(new Haunted(hauntDur));
            target.ApplyEffect(new Haunted(hauntDur), owner);
            duration = cooldown - owner.cdReduction;
        }
    }


    public override void OnTurnFinish()
    {
        if(duration > 0)
        {
            duration--;
        }
    }
    
}
