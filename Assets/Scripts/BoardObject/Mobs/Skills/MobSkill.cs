using UnityEngine;

public abstract class MobSkill
{

    public BoardMob owner;
    public int cost, cooldown, duration;
    public string skillName, skillDesc;
    public SkillData data;
    public bool used;

    
    public virtual void OnTurnStart() { }
    public virtual void OnTurnEnd() { }
    public virtual void OnTurnFinish() { }
    public virtual void ApplyEffect(BoardMob target ) { }
    public virtual void OnActionDone() { }

    public virtual void RemoveEffect(BoardMob target) { }

    public virtual void ResetEffect() { }


}
