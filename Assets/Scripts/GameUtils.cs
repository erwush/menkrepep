using UnityEngine;
using System.Collections.Generic;


public static class GameUtils
{
    public static List<Tile> GetValidTiles(Tile currentTile, Vector2Int[] directions)
    {
        BoardManager board = BoardManager.Instance;
        List<Tile> validTiles = new List<Tile>();

        foreach (var dir in directions)
        {
            Tile tile = board.GetTile(
                currentTile.x + dir.x,
                currentTile.y + dir.y
            );

            if (tile != null)
            {
                validTiles.Add(tile);
                tile.isHighlighted = true;
            }
        }

        return validTiles;
    }

    public static List<Tile> GetValidTargets(Tile currentTile, Vector2Int[] directions, int range)
    {
        BoardManager board = BoardManager.Instance;
        List<Tile> validTiles = new List<Tile>();

        foreach (var dir in directions)
        {
            for (int i = 1; i <= range; i++)
            {
                Tile tile = board.GetTile(
               currentTile.x + dir.x * i,
               currentTile.y + dir.y * i
           );

                if (tile != null)
                {
                    validTiles.Add(tile);
                    tile.isHighlighted = true;
                    if (tile.isOccupied) break;
                }


            }
        }

        foreach (var tile in validTiles)
        {
            if (tile.isOccupied && !tile.activeObj.isHightlight) tile.activeObj.ToggleHightlight();
        }

        return validTiles;
    }
}
