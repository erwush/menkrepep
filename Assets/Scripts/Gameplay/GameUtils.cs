using UnityEngine;
using System.Collections.Generic;


public static class GameUtils
{
    //?nbsp  = non breaking space, bair textnya gk kepisah pindah baris. kalau pindah bakaln 1 kalimat
    public const string NBSP = "\u00A0";
    public static List<Tile> GetValidTiles(Tile currentTile, Vector2Int[] directions, int range, bool hightlight, bool ignoreOccupied = false)
    {
        BoardManager board = BoardManager.Instance;
        List<Tile> validTiles = new List<Tile>();

        foreach (var dir in directions)
        {

            //? biar scalable sama range dan gk perlu nambahin satu2 di array
            for (int i = 1; i <= range; i++)
            {

                Tile tile = board.GetTile(
                currentTile.x + dir.x * i,
                currentTile.y + dir.y * i
                );

                if (tile != null && (!tile.isOccupied || ignoreOccupied))
                {
                    validTiles.Add(tile);
                    if (hightlight) tile.isHighlighted = true;
                }
            }
        }

        return validTiles;
    }
    
    public static List<Tile> GetAreaTiles(Tile currentTile, int range, bool hightlight, bool ignoreOccupied = false)
    {
        BoardManager board = BoardManager.Instance;
        List<Tile> validTiles = new List<Tile>();

        for (int x = currentTile.x - range; x <= currentTile.x + range; x++)
        {
            for (int y = currentTile.y - range; y <= currentTile.y + range; y++)
            {
                Tile tile = board.GetTile(x, y);
                if (tile != null && (!tile.isOccupied || ignoreOccupied))
                {
                    validTiles.Add(tile);
                    if (hightlight) tile.isHighlighted = true;
                }
            }
        }
        
        return validTiles;
    }

    public static List<Tile> GetValidTargets(Tile currentTile, Vector2Int[] directions, int range, bool highlight)
    {
        BoardManager board = BoardManager.Instance;
        List<Tile> validTiles = new List<Tile>();

        foreach (var dir in directions)
        {
            //? biar scalable sama range dan gk perlu nambahin satu2 di array
            for (int i = 1; i <= range; i++)
            {
                Tile tile = board.GetTile(
                currentTile.x + dir.x * i,
                currentTile.y + dir.y * i
                );

                if (tile != null)
                {
                    validTiles.Add(tile);
                    if (highlight) tile.isHighlighted = true;
                    if (tile.isOccupied) break;
                }


            }
        }

        foreach (var tile in validTiles)
        {
            if (tile.isOccupied && !tile.activeObj.isHightlight && highlight) tile.activeObj.ToggleHighlight();
        }

        return validTiles;
    }
    
    public static float CalculateMobDamage(BoardMob source, BoardMob target, bool ignoreArmor = false)
    {
        float damage = source.finalAtk;
        if(!ignoreArmor) damage -= target.armor;
        foreach (var status in source.statusEffects)
        {
            damage = status.ModifyValue(ModifyType.DamageDealt, damage);
        }
        return damage;
    }
}
