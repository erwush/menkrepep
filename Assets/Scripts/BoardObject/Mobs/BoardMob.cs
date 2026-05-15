using Unity.Mathematics;
using System.Collections.Generic;
using UnityEngine;
using Utils = GameUtils;

public abstract class BoardMob : BoardObject
{
    public MobData data;
    public float hp, atk;
    public int atkRange;
    public List<Tile> validTiles = new List<Tile>();
    public List<BoardObject> validTarget = new List<BoardObject>();
    private TurnManager turn = TurnManager.Instance;

    public virtual void Start()
    {
        board = BoardManager.Instance;
        owner = TurnManager.Instance.activePlayer;
        owner.activeUnits.Add(this.gameObject);
        hp = data.maxHp;
        atk = data.atk;
        atkRange = data.atkRange;

    }

    public virtual void ChangeHealth(float amount)
    {
        hp += amount;
        if (hp > data.maxHp) hp = data.maxHp;
        if (hp < 0) hp = 0;
    }

    public virtual void Attack(BoardMob target)
    {
        if (validTarget.Contains(target))
        {

            target.ChangeHealth(-atk);

            owner.ChangeState("Idle");

            owner.selectedTile = null;
            ResetTiles();
        }
    }

    public virtual void Move()
    {
        if (validTiles.Count > 0 && validTiles.Contains(owner.selectedTile))
        {
            foreach (var dir in data.moveDir)
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

    public void ResetTiles()
    {
        foreach (var tile in validTiles)
        {
            tile.isHighlighted = false;
            if (owner.prevState == ActionState.Attack) if (tile.isOccupied && tile.activeObj.isHightlight) tile.activeObj.ToggleHightlight();
        }
        if(validTarget.Count > 0) validTarget.Clear();
        validTiles.Clear();

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
                validTiles = Utils.GetValidTiles(currentTile, data.moveDir);

            }
            else if (owner.actState == ActionState.Attack)
            {
                if (owner.selectedObj != gameObject && owner.activeUnits.Contains(owner.selectedObj)) owner.selectedObj.GetComponent<BoardObject>().UnselectThis();
                owner.selectedObj = gameObject;
                validTiles = Utils.GetValidTargets(currentTile, data.atkDir, data.atkRange);
                foreach (var tile in validTiles) if (tile.isOccupied) validTarget.Add(tile.activeObj);

            }
        }
        else
        {

            if (turn.activePlayer.actState == ActionState.Attack && turn.activePlayer.selectedObj != null)
            {

                turn.activePlayer.selectedObj.GetComponent<BoardMob>().Attack(this);
            }
        }
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