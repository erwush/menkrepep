using UnityEngine;
using System.Collections.Generic;
using Utils = GameUtils;
using System;

public class Loom : BoardBlock
{

    public BannerPattern item;
    public Action<BoardMob> applyBuff;
    public Action<BoardMob> removeBuff;
    public override void Start()
    {
        effectRange = Data.effectRange;
        targetTiles = Utils.GetValidTiles(currentTile, Data.effectDir, effectRange, false, true);
        base.Start();
        // OnActionEnd();

    }

    public override void OnActionDone()
    {
        List<BoardMob> currentTargets = new();
        foreach (var tile in targetTiles)
        {
            if (TryGetUnit<BoardMob>(tile, out BoardMob mob))
            {

                if (mob.owner != owner) continue;



                currentTargets.Add(mob);

                if (!targetUnits.Contains(mob))
                {
                    targetUnits.Add(mob);
                    applyBuff(mob);
                    mob.Recalculate();

                }
            }
        }


        for (int i = targetUnits.Count - 1; i >= 0; i--)
        {
            if (targetUnits[i] is BoardMob mob)
            {

                if (!currentTargets.Contains(mob))
                {
                    removeBuff(mob);
                    mob.Recalculate();
                    targetUnits.RemoveAt(i);
                }
            }

        }
    }
}

