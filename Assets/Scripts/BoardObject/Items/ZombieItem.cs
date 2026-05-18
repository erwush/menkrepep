using Unity.VisualScripting;
using UnityEngine;


public class ZombieItem : Item
{

    public override void SetItem(BoardMob target)
    {
        if (target is not Zombie)
        {
            target.owner.selectedObj = null;
            return;
        }
        target.owner.star -= data.cost;
        int dice = Random.Range(1, 10);
        if (dice <= 9)
        {
            
            ZombieItem item = target.AddComponent<ZombieItem>();
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
