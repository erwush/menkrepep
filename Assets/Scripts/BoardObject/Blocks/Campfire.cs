using UnityEngine;
using System.Collections.Generic;

public class Campfire : BoardBlock
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void OnTurnStart()
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
                }       
            }
        }
        for (int i = targetUnits.Count - 1; i >= 0; i--)
        {
            if (targetUnits[i] is BoardMob mob)
            {
                if (!currentTargets.Contains(mob))
                {
                    targetUnits.RemoveAt(i);
                }
            }

        }
        foreach(var mob in currentTargets) mob.ChangeHealth(5);
    }
}
