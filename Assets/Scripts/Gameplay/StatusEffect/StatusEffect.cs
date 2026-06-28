using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffect", menuName = "StatusEffect")]
public class StatusEffect
{
    public string statusName;
    [TextArea(3, 10)] public string statusDesc;
    public BoardObject source, owner;
    public int startDuration, turnDuration, effectLevel;
    public EffectTag effectTag;
    public DurationType durationType;

    public virtual void OnSelfTurnStart()
    {
        if (durationType == DurationType.SelfTurnStart)
        {
            if (turnDuration > 0) turnDuration--;
            if (turnDuration <= 0) RemoveEffect();
        }
    }

    public virtual void OnSelfTurnEnd()
    {
        if (durationType == DurationType.SelfTurnEnd)
        {
            if (turnDuration > 0) turnDuration--;
            if (turnDuration <= 0) RemoveEffect();
        }
    }

    public virtual void OnAnyTurnStart()
    {
        if (durationType == DurationType.AnyTurnStart)
        {
            if (turnDuration > 0) turnDuration--;
            if (turnDuration <= 0) RemoveEffect();
        }
    }


    public virtual void OnAnyTurnEnd()
    {
        if (durationType == DurationType.AnyTurnEnd)
        {
            if (turnDuration > 0) turnDuration--;
            if (turnDuration <= 0) RemoveEffect();
        }
    }
    public virtual void ApplyEffect(BoardMob target) { }
    public virtual void OnActionDone() { }

    public virtual void RemoveEffect()
    {
        owner.statusEffects.Remove(this);
    }

    public virtual void ResetEffect() { }
    public virtual float ModifyValue(ModifyType type, float value) { return value; }

}

public enum ModifyType
{
    DamageDealt,
    DamageTaken,

}

public enum DurationType
{
    SelfTurnStart,
    SelfTurnEnd,
    AnyTurnStart,
    AnyTurnEnd,

}