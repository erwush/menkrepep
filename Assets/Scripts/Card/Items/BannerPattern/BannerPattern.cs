using System;
using UnityEngine;

public abstract class BannerPattern : Item
{



    public override void SetItem(BoardBlock target)
    {
        Loom loom = target as Loom;
        loom.item = this;
        loom.applyEffect = ApplyEffect;
        loom.removeEffect = RemoveEffect;



        target.owner.EndAction();
    }

    public override void OnThrown()
    {
        Loom loom = holder as Loom;

        foreach(BoardMob mob in loom.targetUnits)
        {
            loom.removeEffect(mob);
            loom.targetUnits.Remove(mob);
        }

        loom.applyEffect = null;
        loom.removeEffect = null;
        holder = null;
    }

    public virtual void ApplyEffect(IDamageable target) { }

    public virtual void RemoveEffect(IDamageable target) { }
    
    public virtual void TriggerEffect(IDamageable target) { }

}
