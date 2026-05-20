using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public abstract class BoardObject : MonoBehaviour
{

    public Tile tile;
    public Tile currentTile;
    public int cost;

    public bool isHightlight;
    public Player owner;
    public Material[] Mat; //0 = default, 1 = hightlighted
    public BoardManager board;
    public TurnManager turn;
    public UiManager menu;
    public UnitType type;
    public ObjectData data;
    public List<StatusEffect> statusEffects = new List<StatusEffect>();
    public List<EffectTag> Immunities;

    public virtual void Awake()
    {
         new List<StatusEffect>();
        if(data != null) cost = data.cost;
        board = BoardManager.Instance;
        turn = TurnManager.Instance;
        menu = UiManager.Instance;

    }
    public int x
    {
        get
        {
            return tile.x;
        }
    }
    public int y
    {
        get
        {
            return tile.y;
        }
    }

    public virtual void SelectThis()
    {
        owner.selectedObj = gameObject;
    }

    public virtual void UnselectThis()
    {
        owner.selectedObj = null;
    }






    public virtual void ToggleHighlight()
    {
        // if (isHightlight)
        // {
        //     Material self = GetComponent<MeshRenderer>().material = Mat[0];
        //     isHightlight = false;
        // }
        // else
        // {
        //     Material self = GetComponent<MeshRenderer>().material = Mat[1];
        //     isHightlight = true;
        // }
    }

    public virtual void ApplyEffect(BoardObject target)
    {
        
        foreach(var player in TurnManager.Instance.players) player.RefreshDisplay();
    }

    //*ON-(CONDITION) EFFECT
    public virtual void OnTurnEnd()
    {
        
        foreach(var player in TurnManager.Instance.players) player.RefreshDisplay();
    }

    public virtual void OnTurnStart()
    {
        
        foreach(var player in TurnManager.Instance.players) player.RefreshDisplay();
    }

    public virtual void OnActionDone()
    {

        foreach (var player in TurnManager.Instance.players) player.RefreshDisplay();
    }
    
    public virtual void OnTurnFinish()
    {
        foreach (var player in TurnManager.Instance.players) player.RefreshDisplay();
    }


}


public enum UnitType
{
    Block,
    Mob,
    Item,
}

public enum EffectTag
{
    Block,
    Debuff,
    Buff,
}