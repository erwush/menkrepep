using UnityEngine;
using System.Collections.Generic;

public class CreeperBannerPattern : BannerPattern
{

    public override void ApplyEffect(IDamageable target)
    {

        target.OnDeath += TriggerEffect;
    }

    public override void TriggerEffect(IDamageable target)
    {

        BoardMob mob = target as BoardMob;
        List<Tile> tiles = GameUtils.GetAreaTiles(mob.currentTile, 1, false);
        foreach(var tile in tiles)
        {
            if (tile.isOccupied && tile.activeObj is BoardMob)
            {
                BoardMob targetMob = tile.activeObj as BoardMob;
                targetMob.ChangeHealth(-(mob.maxHp * 0.2f));
            }
        }

    }

}