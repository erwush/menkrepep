using UnityEngine;

public class NormalAttack : MobSkill
{

    public NormalAttack(BoardMob owner)
    {
        this.owner = owner;
        data = owner.skillData[0];
        skillName = data.skillName;
        skillDesc = data.skillDesc;
        cost = data.cost;
        cooldown = data.cooldown;
    }
    public override void ApplyEffect(BoardMob target)
    {
        owner.Attack(target);
    }

}
