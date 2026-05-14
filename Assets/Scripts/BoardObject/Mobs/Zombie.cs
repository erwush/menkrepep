using UnityEngine;
using System.Collections.Generic;
using Utils = GameUtils;

public class Zombie : BoardMob
{

    public override Vector2Int[] atkDir =>
    new Vector2Int[]
    {
        new Vector2Int(0, 1),   // atas
        new Vector2Int(0, -1),  // bawah
        new Vector2Int(1, 0),   // kanan
        new Vector2Int(-1, 0),  // kiri
        
    };
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
            player.ChangeState("Idle");
            transform.position = new Vector3(player.selectedTile.transform.position.x, player.selectedTile.transform.position.y + 1.5f, player.selectedTile.transform.position.z);
            currentTile = player.selectedTile;
            player.selectedTile = null;
            ResetTiles();
        }
    }

    public override void Attack()
    {
        if(validTiles.Count > 0 && validTiles.Contains(player.selectedTile))
        {
            BoardMob target = player.selectedTile.activeObj.GetComponent<BoardMob>();
            target.ChangeHealth(-atk);

            player.ChangeState("Idle");

            player.selectedTile = null;
            ResetTiles();
        }
    }


    public override void SelectThis()
    {
        ResetTiles();
        if (player.actState == ActionState.Move)
        {
            
            if (player.selectedObj != gameObject && player.activeUnits.Contains(player.selectedObj)) player.selectedObj.GetComponent<BoardObject>().UnselectThis();
            player.selectedObj = gameObject;
            validTiles = Utils.GetValidTiles(board, currentTile, moveDir);

        }
        else if (player.actState == ActionState.Attack)
        {
            if (player.selectedObj != gameObject && player.activeUnits.Contains(player.selectedObj)) player.selectedObj.GetComponent<BoardObject>().UnselectThis();
            player.selectedObj = gameObject;
            validTiles = Utils.GetValidTargets(board, currentTile, atkDir);

        }

    }
    
    public void ResetTiles()
    {
        foreach (var tile in validTiles)
        {
            tile.isHighlighted = false;
            if(player.prevState == ActionState.Attack) if(tile.isOccupied && tile.activeObj.isHightlight) tile.activeObj.ToggleHightlight();
        }
        validTiles.Clear();

    }
    public override void UnselectThis()
    {
        foreach (var tile in validTiles)
        {
            tile.isHighlighted = false;
            if (player.actState == ActionState.Attack) if (tile.isOccupied) tile.activeObj.ToggleHightlight();
        }
        ResetTiles();
        player.selectedObj = null;
        
    }


}
