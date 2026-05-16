using System.Collections.Generic;
using UnityEngine;
using Utils = GameUtils;

public abstract class BoardBlock : BoardObject
{

    public BlockData data;
    public int effectRange;
    public List<Tile> targetTiles = new();
    public List<BoardObject> targetUnits = new();



    public virtual void Start()
    {
        effectRange = data.effectRange;
        board = BoardManager.Instance;
        // owner.activeUnits.Add(this.gameObject);
        turn = TurnManager.Instance;
        owner = TurnManager.Instance.activePlayer;
        
        type = UnitType.Block;

        owner.EndAction();
    }

    public virtual void WhenHoverEnter()
    {
        foreach (var tile in targetTiles)
        {
            tile.isHighlighted = true;
        }
    }

    public virtual void WhenHoverExit()
    {
        foreach (var tile in targetTiles)
        {
            tile.isHighlighted = false;
        }
    }

    // public bool TryGetMob(Tile tile, out BoardMob mob)
    // {
    //     mob = null;

    //     if (!tile.isOccupied)
    //         return false;

    //     BoardObject unit = tile.activeObj.GetComponent<BoardObject>();

    //     if (unit.owner != owner)
    //         return false;

    //     mob = unit as BoardMob;

    //     return mob != null;
    // }

    // public bool TryGetBlock(Tile tile, out BoardBlock block)
    // {
    //     block = null;

    //     if (!tile.isOccupied)
    //         return false;

    //     BoardObject unit = tile.activeObj.GetComponent<BoardObject>();

    //     if (unit.owner != owner)
    //         return false;

    //     block = unit as BoardBlock;

    //     return block != null;
    // }

    //? t itu intinya kek bebas atau bisa diisi variable sedangkan bagian where T : () itu berarti T harus turunan atau childboardobject
    //? where semacam pembatas
    public bool TryGetUnit<T>(Tile tile, out T result) where T : BoardObject
    {
        result = null;

        if (!tile.isOccupied)
            return false;

        BoardObject unit = tile.activeObj;


        result = unit as T;

        return result != null;
    }




}
