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

    public static List<Tile> GetValidTargets(Tile currentTile, Vector2Int[] directions)
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

        foreach(var tile in validTiles)
        {
            if (tile.isOccupied && !tile.activeObj.isHightlight) tile.activeObj.ToggleHightlight();
        }

        return validTiles;
    }
}
