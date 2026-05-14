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
    public List<BoardObject> validTarget = new List<BoardObject>();
    private TurnManager turn = TurnManager.Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        board = BoardManager.Instance;
        owner = TurnManager.Instance.activePlayer;
        owner.activeUnits.Add(this.gameObject);



    }
    public override void Move()
    {
        if (validTiles.Count > 0 && validTiles.Contains(owner.selectedTile))
        {
            foreach (var dir in moveDir)
            {
                board.tiles[currentTile.x + dir.x, currentTile.y + dir.y].isHighlighted = false;
            }
            currentTile.isOccupied = false;
            owner.selectedTile.isOccupied = true;
            owner.ChangeState("Idle");
            transform.position = new Vector3(owner.selectedTile.transform.position.x, owner.selectedTile.transform.position.y + 1.5f, owner.selectedTile.transform.position.z);
            currentTile = owner.selectedTile;
            owner.selectedTile = null;
            ResetTiles();
        }
    }

    public override void Attack(BoardMob target)
    {

        if (validTarget.Contains(target))
        {
            
            target.ChangeHealth(-atk);

            owner.ChangeState("Idle");

            owner.selectedTile = null;
            ResetTiles();
        }
        
    }


    public override void SelectThis()
    {
     
        ResetTiles();
        if (turn.activePlayer == owner)
        {
            
            if (owner.actState == ActionState.Move)
            {

                if (owner.selectedObj != gameObject && owner.activeUnits.Contains(owner.selectedObj)) owner.selectedObj.GetComponent<BoardObject>().UnselectThis();
                owner.selectedObj = gameObject;
                validTiles = Utils.GetValidTiles(currentTile, moveDir);

            }
            else if (owner.actState == ActionState.Attack)
            {
                if (owner.selectedObj != gameObject && owner.activeUnits.Contains(owner.selectedObj)) owner.selectedObj.GetComponent<BoardObject>().UnselectThis();
                owner.selectedObj = gameObject;
                validTiles = Utils.GetValidTargets(currentTile, atkDir);
                foreach(var tile in validTiles) if(tile.isOccupied) validTarget.Add(tile.activeObj);

            }
        } else
        {
            
            if(turn.activePlayer.actState == ActionState.Attack && turn.activePlayer.selectedObj != null)
            {
      
                turn.activePlayer.selectedObj.GetComponent<Zombie>().Attack(this);
            }
        }

    }

    public void ResetTiles()
    {
        foreach (var tile in validTiles)
        {
            tile.isHighlighted = false;
            if (owner.prevState == ActionState.Attack) if (tile.isOccupied && tile.activeObj.isHightlight) tile.activeObj.ToggleHightlight();
        }
        validTiles.Clear();

    }
    public override void UnselectThis()
    {
        foreach (var tile in validTiles)
        {
            tile.isHighlighted = false;
            if (owner.actState == ActionState.Attack) if (tile.isOccupied) tile.activeObj.ToggleHightlight();
        }
        ResetTiles();
        owner.selectedObj = null;

    }


}
