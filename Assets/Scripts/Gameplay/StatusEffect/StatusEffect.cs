using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffect", menuName = "StatusEffect")]
public class StatusEffect
{
    public string statusName;
    public BoardObject source, owner;
    public int turnDuration;
    public EffectTag effectTag;

    public virtual void OnTurnStart() { }
    public virtual void OnTurnEnd() { }
    public virtual void OnTurnFinish() {}
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