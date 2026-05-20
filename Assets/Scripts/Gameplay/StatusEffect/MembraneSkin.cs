using UnityEngine;

public class MembraneSkin : StatusEffect
{


    public BoardMob Owner => owner as BoardMob;
    public bool isReducing;

    public MembraneSkin()
    {
        turnDuration = 3;
        effectTag = EffectTag.Buff;
    }

    public override void OnTurnStart()
    {
        if (turnDuration > 0) turnDuration--;
        if (turnDuration <= 0) RemoveEffect();
    }



    public override void RemoveEffect()
    {
        Owner.bonusAtk += 4;
        owner.statusEffects.Remove(this);
    }

    public override void ApplyEffect(BoardMob target)
    {
        Owner.bonusAtk -= 4;
        turnDuration = 3;
    }

    public override void ResetEffect()
    {
        turnDuration = 3;
    }
    

}
