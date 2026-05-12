using Unity.Mathematics;
using UnityEngine;

public abstract class BoardMob : BoardObject
{
    public int hp
    {
        set
        {
            if (value > maxHp)
            {
                hp = maxHp;
            }
            else
            {
                hp = value;
            }
        }
    }
    public abstract Vector2Int[] moveDir { get; }
    public int maxHp;
    public int atk;

}