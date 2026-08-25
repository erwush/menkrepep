using Unity.VisualScripting;
using UnityEngine;

public class PhantomItem : Item
{
    public override void SetItem(BoardMob target)
    {
        if (target.heldItem != null) return;
        if (target is not Phantom)
        {
            target.owner.selectedObj = null;
            return;
        }
        target.owner.star -= data.cost;
        int dice = Random.Range(1, 10);
        if (dice == 9)
        {
            PhantomItem item = target.AddComponent<PhantomItem>();
            item.data = Data;
            item.type = UnitType.Item;
            item.holder = target;
            target.heldItem = item;
            item.OnHeld();
        }
        target.owner.selectedObj = null;
        target.owner.EndAction();
    }

    public override void OnHeld()
    {
        if (holder is Phantom phantom)
        {
            phantom.maxHp += 5;
            phantom.ChangeHealth(5);
            phantom.bonusAtk += 3;
            phantom.Immunities.Add(EffectTag.Debuff);
            phantom.Immunities.Add(EffectTag.Block);
        }
    }

    public override void OnThrown()
    {
        if (holder is Phantom phantom)
        {
            phantom.maxHp -= 5;
            phantom.bonusAtk -= 3;
            phantom.Immunities.Remove(EffectTag.Debuff);
            phantom.Immunities.Remove(EffectTag.Block);
        }
    }
}
