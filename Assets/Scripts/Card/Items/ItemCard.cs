using UnityEngine;

public abstract class ItemCard : IPlayable
{

    public int amount;
    public BoardObject holder;
    public ItemData data;


    public int cost;

    public bool isHightlight;
    public Player owner { get; set; }
    public Material[] Mat; //0 = default, 1 = hightlighted
    public UnitType type;

    




    public virtual void SetItem(BoardBlock target) { }

    public virtual void SetItem(BoardMob target) { }

    public virtual void OnHeld() { }

    public virtual void OnThrown() { }

    public virtual void UseItem() { }
    

    

    public virtual void ApplyEffect(BoardObject target)
    {

        foreach (var player in TurnManager.Instance.players) player.RefreshDisplay();
    }

    //*ON-(CONDITION) EFFECT
    public virtual void OnSelfTurnEnd()
    {

        foreach (var player in TurnManager.Instance.players) player.RefreshDisplay();
    }

    public virtual void OnSelfTurnStart()
    {

        foreach (var player in TurnManager.Instance.players) player.RefreshDisplay();
    }

    public virtual void OnActionDone()
    {

        foreach (var player in TurnManager.Instance.players) player.RefreshDisplay();
    }

    public virtual void OnAnyTurnStart()
    {
        foreach (var player in TurnManager.Instance.players) player.RefreshDisplay();
    }

    public virtual void OnAnyTurnEnd()
    {
        foreach (var player in TurnManager.Instance.players) player.RefreshDisplay();
    }

}
