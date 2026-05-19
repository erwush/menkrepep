using UnityEngine;

public class Haunted : StatusEffect
{

     public Haunted(int dur, BoardMob owner)
    {
        this.owner = owner;
        turnDuration = dur;
        effectTag = EffectTag.Debuff;
        Debug.Log("dur" + turnDuration);
        Debug.Log("owner: " + owner);
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
