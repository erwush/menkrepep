using System;
using UnityEngine;

public abstract class BannerPattern : Item
{

    public Action Buff;

    public override void SetItem(BoardBlock target)
    {
        Loom loom = target as Loom;
        loom.item = this;



        target.owner.EndAction();
    }

    public virtual void ApplyEffect(IDamageable target) { }

    public virtual void RemoveEffect(IDamageable target) { }
    
    public virtual void TriggerEffect(IDamageable target) { }

}
