using UnityEngine;



public abstract class MobSkill
{

    public BoardMob owner;
    //?cur = current
    public int cost, ultCost, cooldown, curCooldown;
    public string skillName;
    [TextArea(3, 10)] public string skillDesc, costDesc;
    public SkillData data;
    public bool used;

    // protected string nbsp = "\u00A0";



    public virtual void OnSelfTurnStart() { }
    public virtual void OnSelfTurnEnd() { }
    public virtual void OnAnyTurnStart() { }
    public virtual void OnAnyTurnEnd() { }
    public virtual void ApplyEffect(BoardMob target) { }
    public virtual void OnActionDone() { }
    public virtual void OnDamageTaken(float amount) { }
    public virtual void OnDamageDealt(float amount) { }
    public virtual void OnHealthChange(float amount) { }

    public virtual float ModifyValue(ModifyType type, float value = 0, float additonalValue = 0) { return value; }

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

    public virtual void RefreshCost() { }



}
