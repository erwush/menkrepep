using Unity.Mathematics;
using System.Collections.Generic;
using UnityEngine;
using Utils = GameUtils;

public abstract class BoardMob : BoardObject
{

    public float maxHp, hp, bonusAtk, atk, finalAtk, spd;
    public int atkRange, moveRange;
    public List<Tile> validTiles = new List<Tile>();
    public List<BoardObject> validTarget = new List<BoardObject>();
    public MobData Data => data as MobData;
    public Item heldItem;


    public virtual void Start()
    {
        
        owner = TurnManager.Instance.activePlayer;
        // owner.activeUnits.Add(this.gameObject);
        
        // owner.EndAction();
        Recalculate();

    }

    public override void Awake()
    {
        base.Awake();
        cost = Data.cost;
        turn = TurnManager.Instance;
        board = BoardManager.Instance;
        hp = Data.maxHp;
        maxHp = Data.maxHp;
        atk = Data.atk;
        atkRange = Data.atkRange;
        moveRange = Data.moveRange;
        type = UnitType.Mob;
        spd = Data.speed;
    }
    

    public virtual void ChangeHealth(float amount)
    {
        Mathf.Round(amount);
        hp += amount;
        if (hp > maxHp) hp = maxHp;
        if (hp < 0) hp = 0;
    }

    public virtual void Recalculate()
    {
        atk = Data.atk;
        finalAtk = atk + bonusAtk;
        
    }
    
    public void ResetStats()
    {
        
    }

    public virtual void Attack(BoardMob target)
    {
        if (validTarget.Contains(target))
        {

            target.ChangeHealth(-finalAtk);

            owner.EndAction();

            owner.selectedTile = null;
            ResetTiles();
        }
    }

    
    public virtual void Move()
    {

        if (validTiles.Count > 0 && validTiles.Contains(owner.selectedTile))
        {

            foreach (var tile in validTiles)
            {
                board.tiles[tile.x, tile.y].isHighlighted = false;
            }
            currentTile.isOccupied = false;
            currentTile.activeObj = null;
            owner.selectedTile.isOccupied = true;
            owner.EndAction();
            transform.position = new Vector3(owner.selectedTile.transform.position.x, owner.selectedTile.transform.position.y + 1.5f, owner.selectedTile.transform.position.z);
            currentTile = owner.selectedTile;
            owner.selectedTile = null;
            ResetTiles();
        }
    }


    //? > Ketika state berubah tile atau unit yang kehighlight akan direset dan mengikuti kondisi
    public void ResetTiles()
    {
        foreach (var tile in validTiles)
        {
            tile.isHighlighted = false;
            if (owner.prevState == ActionState.Attack) if (tile.isOccupied && tile.activeObj.isHightlight) tile.activeObj.ToggleHightlight();
        }
        if (validTarget.Count > 0) validTarget.Clear();
        validTiles.Clear();

    }

    public void SetItem(Item target)
    {
        
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
                validTiles = Utils.GetValidTiles(currentTile, Data.moveDir, moveRange, true);

            }
            else if (owner.actState == ActionState.Attack)
            {
                if (owner.selectedObj != gameObject && owner.activeUnits.Contains(owner.selectedObj)) owner.selectedObj.GetComponent<BoardObject>().UnselectThis();
                owner.selectedObj = gameObject;
                validTiles = Utils.GetValidTargets(currentTile, Data.atkDir, atkRange, true);
                foreach (var tile in validTiles) if (tile.isOccupied) validTarget.Add(tile.activeObj);
            } else if(owner.actState == ActionState.Place && owner.selectedObj.GetComponent<BoardObject>().type == UnitType.Item) owner.selectedObj.GetComponent<Item>().SetItem(this);
        }
        else
        {
            //? kalau unitnya beda owner dengna turn sekarang berarti pengen nyerang dan bukan select
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