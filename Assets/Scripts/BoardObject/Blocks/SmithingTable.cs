using UnityEngine;
using System.Collections.Generic;

public class SmithingTable : BoardBlock
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        // OnActionEnd();

    }

    public override void OnActionEnd()
    {
        List<BoardMob> currentTargets = new();
        foreach (var tile in targetTiles)
        {
            if (tile.isOccupied)
            {
                BoardObject unit = tile.activeObj.GetComponent<BoardObject>();
                if (unit.owner != owner) continue;
                BoardMob mob = unit as BoardMob;

                if (mob == null)
                    continue;

                currentTargets.Add(mob);

                if (!targetUnits.Contains(mob))
                {
                    targetUnits.Add(mob);
                    mob.bonusAtk += 5;
                    mob.Recalculate();
                    Debug.Log("ngono");
                }
            }
        }
        Debug.Log("bb"+currentTargets.Count);
        for (int i = targetUnits.Count - 1; i >= 0; i--)
        {

            BoardObject unit = targetUnits[i];

            if (unit is BoardMob mob)
            {
                if (!currentTargets.Contains(mob))
                {
                    mob.bonusAtk -= 5;
                    Debug.Log("anu");
                    mob.Recalculate();
                    targetUnits.RemoveAt(i);
                }
            }
        }
        Debug.Log("aa"+currentTargets.Count);
    }
}

