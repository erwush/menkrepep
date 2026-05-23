using UnityEngine;
using System.Collections.Generic;
using Utils = GameUtils;

public class Skeleton : BoardMob
{
    public HashSet<int> validRolls = new HashSet<int>();
    public int critValue;

    // public override Vector2Int[] atkDir =>
    //     new Vector2Int[]
    //     {
    //     //Vector2 Int all
    //     new (0, 1),   // atas
    //     new (0, 2), // atas*2
    //     new (0, 3), // atas*3
    //     new (0, -1),  // bawah
    //     new (0, -2), // bawah*2
    //     new (0, -3), // bawah*3
    //     new (1, 0),   // kanan
    //     new (2, 0), // kanan*2
    //     new (3, 0), // kanan*3
    //     new (-1, 0),  // kiri
    //     new (-2, 0), // kiri*2
    //     new (-3, 0), // kiri*3

    //     };
    // public override Vector2Int[] moveDir =>
    // new Vector2Int[]{
    //     new (0, 1),   // atas
    //     new (0, -1),  // bawah
    //     new (1, 0),   // kanan
    //     new (-1, 0),  // kiri

    //     new (1, 1),   // kanan atas
    //     new (1, -1),  // kanan bawah
    //     new (-1, 1),  // kiri atas
    //     new (-1, -1), // kiri bawah
    // };

    public override void Awake()
    {
        base.Awake();
        skills.Add(new NormalAttack(this));
        skills.Add(new ChargedShot(this));
        skills.Add(new OverchargedShot(this));
        validRolls.Add(1);
        validRolls.Add(9);
        critValue = 3;
    }
    public override void Attack(BoardMob target)
    {
        if (validTarget.Contains(target))
        {
            int dice = Random.Range(1, 10);
            float dmg = finalAtk;
            if (validRolls.Contains(dice))
            {
                dmg *= critValue;
            }
            dmg = Utils.CalculateMobDamage(dmg, this, target);
            target.ChangeHealth(-dmg);

            owner.selectedTile = null;
            ResetTiles();
        }
    }




}
