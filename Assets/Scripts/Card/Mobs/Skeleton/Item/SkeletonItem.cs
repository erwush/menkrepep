using Unity.VisualScripting;
using UnityEngine;

public class SkeletonItem : ItemCard
{

    public SkeletonItem(BoardMob target)
    {
        data = BoardManager.Instance.GetItemData("SkeletonItem");
        InitializeItem(target);
    }


    public override void SetItem(BoardMob target)
    {
        if (target.heldItem != null) return;
        if (target is not Skeleton)
        {
            target.owner.selectedObj = null;
            return;
        }
        target.owner.star -= data.cost;
        int dice = Random.Range(1, 10);
        if (dice == 9)
        {
            holder = target;
            target.heldItem = this;
            owner = holder.owner;
            OnHeld();
        }
        target.owner.selectedObj = null;

        target.owner.EndAction();
    }
    public override void OnHeld()
    {
        if (holder is Skeleton skeleton)
        {
            skeleton.validRolls.Add(5);
            skeleton.bonusAtk += 2 + amount;
            skeleton.atkRange += 1 + amount;
        }
    }

    public override void OnThrown()
    {
        if (holder is Skeleton skeleton)
        {
            skeleton.validRolls.Remove(5);
            skeleton.bonusAtk -= 2 + amount;
            skeleton.atkRange -= 1 + amount;
        }
    }

}
