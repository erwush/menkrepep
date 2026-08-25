using UnityEngine;
using System.Collections.Generic;
using Utils = GameUtils;

public class SmithingTable : BoardBlock
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
                    mob.bonusAtk += 5;
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
                    mob.bonusAtk -= 5;

                    mob.Recalculate();
                    targetUnits.RemoveAt(i);
                }
            }

        }
    }
}

