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
        targetTiles = Utils.GetValidTiles(currentTile, data.effectDir, effectRange, false, true);
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

    // public bool CheckTarget()
    // {

    // }



}
