using Unity.VisualScripting;
using UnityEngine;


public class ZombieItem : ItemCard
{

    public ZombieItem(BoardMob target)
    {
        data = BoardManager.Instance.GetItemData("ZombieItem");
        InitializeItem(target);
    }

    public override void SetItem(BoardMob target)
    {
        if (target.heldItem != null) return;

        if (target is not Zombie)
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
        base.OnHeld();
        if (holder is Zombie zombie)
        {
            zombie.maxHp += 3 + amount;
            zombie.ChangeHealth(3 + amount);
            zombie.bonusAtk += 3 + amount;
            if (zombie.hp <= 5)
            {
                zombie.bonusAtk += 3 + amount;
                zombie.spd += 2 + amount;
            }
        }
    }

    public override void OnThrown()
    {
        base.OnThrown();
        if (holder is Zombie zombie)
        {
            zombie.maxHp -= 3 + amount;
            zombie.bonusAtk -= 3 + amount;
            if (zombie.hp <= 5)
            {
                zombie.bonusAtk -= 3 + amount;
                zombie.spd -= 2 + amount;
            }
        }
    }
}
