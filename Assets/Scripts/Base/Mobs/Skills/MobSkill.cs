using UnityEngine;

public abstract class MobSkill
{

    public BoardMob owner;
    public int cost, ultCost, cooldown, duration;
    public string skillName, skillDesc, costDesc;
    public SkillData data;
    public bool used;
    
    // protected string nbsp = "\u00A0";



    public virtual void OnTurnStart() { }
    public virtual void OnTurnEnd() { }
    public virtual void OnTurnFinish() { }
    public virtual void ApplyEffect(BoardMob target) { }
    public virtual void OnActionDone() { }

    public virtual void RemoveEffect(BoardMob target) { }

    public virtual void ResetEffect() { }

    public virtual void OnSelected()
    {
        int range = data.atkRange + owner.atkRange;
        if (data.rangeType == RangeType.Single)
        {
            owner.validTiles = GameUtils.GetValidTargets(owner.currentTile, data.atkDir, range, true);
        }
        else if (data.rangeType == RangeType.Area)
        {
            owner.validTiles = GameUtils.GetAreaTiles(owner.currentTile, range, true, true);
        }
        owner.HighlightTarget();
    }

    public virtual void OnUnselected()
    {
        foreach (var tile in owner.validTiles)
        {
            tile.isHighlighted = false;
            if (owner.owner.actState == ActionState.Attack) if (tile.isOccupied) tile.activeObj.ToggleHighlight();
        }
    }

    public virtual void RefreshCost() {}

}
