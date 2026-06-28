using UnityEngine;

public class Weakness : StatusEffect
{


    public BoardMob Owner => owner as BoardMob;
    // public bool isReducing;
    

    public Weakness(int duration, int level, DurationType durType)
    {
        startDuration = duration;
        turnDuration = duration;
        effectLevel = level;
        effectTag = EffectTag.Debuff;
        durationType = durType;
    }




    public override void RemoveEffect()
    {
        Owner.bonusAtk += 1*effectLevel;
        owner.statusEffects.Remove(this);
    }

    public override void ApplyEffect(BoardMob target)
    {
        Owner.bonusAtk -= 1*effectLevel;
        turnDuration = startDuration;
    }

    public override void ResetEffect()
    {
        turnDuration = startDuration;
    }
    
    
    
    

}
