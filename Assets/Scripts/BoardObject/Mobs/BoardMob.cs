using Unity.Mathematics;
using UnityEngine;

public abstract class BoardMob : BoardObject
{
    public int hp;

    
    public abstract Vector2Int[] moveDir { get; }
    public abstract Vector2Int[] atkDir { get;  }
    public int maxHp;
    public int atk;

    public virtual void ChangeHealth(int amount)
    {
        hp += amount;
        if (hp > maxHp) hp = maxHp;
        if (hp < 0) hp = 0;
    }

        public virtual void Attack(BoardMob target)
    {
        
    }

}