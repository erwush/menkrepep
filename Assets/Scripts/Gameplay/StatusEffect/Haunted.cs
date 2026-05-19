using UnityEngine;

public class Haunted : StatusEffect
{

     public Haunted(int dur)
    {
        turnDuration = dur;
        effectTag = EffectTag.Debuff;   
    }
    public override float ModifyValue(ModifyType type, float value)
    {
        if (type == ModifyType.DamageDealt)
        {
            value = Mathf.Min(value, 5);
        }

        return value;
    }

    public override void OnTurnFinish()
    {
        turnDuration--;
        if(turnDuration <= 0) RemoveEffect();
    }
}
