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
        critValue = (owner as Skeleton).critValue + 1;


    }
    public override void ApplyEffect(BoardMob target)
    {
        if (owner.validTarget.Contains(target))
        {
            if (owner.owner.star >= cost)
            {
                owner.owner.ChangeStar(-cost);
                int dice = Random.Range(1, 10);
                float dmg = owner.finalAtk;
                if ((owner as Skeleton ).validRolls.Contains(dice))
                {
                    dmg *= critValue;
                }
                dmg = GameUtils.CalculateMobDamage(owner, target);
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
