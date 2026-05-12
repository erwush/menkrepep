using UnityEngine;
using System.Collections.Generic;

public class Zombie : BoardMob
{


    public override Vector2Int[] moveDir =>
      new Vector2Int[]{
        new Vector2Int(0, 1),   // atas
        new Vector2Int(0, -1),  // bawah
        new Vector2Int(1, 0),   // kanan
        new Vector2Int(-1, 0),  // kiri

        new Vector2Int(1, 1),   // kanan atas
        new Vector2Int(1, -1),  // kanan bawah
        new Vector2Int(-1, 1),  // kiri atas
        new Vector2Int(-1, -1), // kiri bawah
    };

    public List<Tile> validTiles = new List<Tile>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        board = GameObject.FindWithTag("System").GetComponent<BoardSystem>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.activeUnits.Add(this.gameObject);

    }
    public override void Move()
    {
        if (validTiles.Count > 0 && validTiles.Contains(player.selectedTile))
        {
            foreach (var dir in moveDir)
            {
                board.tiles[currentTile.x + dir.x, currentTile.y + dir.y].isHighlighted = false;
            }
            currentTile.isOccupied = false;
            player.selectedTile.isOccupied = true;
            player.isMoving = false;
            transform.position = new Vector3(player.selectedTile.transform.position.x, player.selectedTile.transform.position.y + 1.5f, player.selectedTile.transform.position.z);
            currentTile = player.selectedTile;
            player.selectedTile = null;
            validTiles.Clear();
        }
    }

    public override void SelectThis()
    {
        if (player.selectedObj != gameObject && player.activeUnits.Contains(player.selectedObj)) player.selectedObj.GetComponent<BoardObject>().UnselectThis();
        player.selectedObj = gameObject;
        foreach (var dir in moveDir)
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
        player.isMoving = true;
    }

    public override void UnselectThis()
    {
        foreach (var tile in validTiles)
        {
            tile.isHighlighted = false;
        }
        validTiles.Clear();
        player.selectedObj = null;
        player.isMoving = false;
    }


}
