using Unity.Mathematics;
using System.Collections.Generic;
using UnityEngine;
using Utils = GameUtils;

public abstract class BoardMob : BoardObject
{

    public float maxHp, hp, bonusAtk, atk, finalAtk, spd, bonusSpd, finalSpd, armor, bonusArmor, finalArmor;
    public int atkRange, moveRange, moveCounter, cdReduction, moveCost;
    //?moveCounter to count how many speed is used each turn
    public List<Tile> validTiles = new List<Tile>();
    public List<BoardObject> validTarget = new List<BoardObject>();
    public MobData Data => data as MobData;
    public Item heldItem;
    public List<MobSkill> skills = new List<MobSkill>();

    public SkillData[] skillData;
    public bool canUlt;


    public virtual void Start()
    {
        canUlt = true;
        owner = TurnManager.Instance.activePlayer;
        // owner.activeUnits.Add(this.gameObject);

        // owner.EndAction();
        finalSpd = spd + bonusSpd;
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
        armor = Data.armor;
        skillData = Data.skillData;
        moveCost = 1;
    }


    public virtual void ChangeHealth(float amount)
    {
        Mathf.Round(amount);
        if (amount < 0)
        {
            foreach (var skill in skills)
            {
                amount = skill.ModifyValue(ModifyType.DamageTaken, amount); 
            }
            
            foreach (var status in statusEffects)
            {
                amount = status.ModifyValue(ModifyType.DamageTaken, amount);
            }
        }
        hp += amount;
        if (hp > maxHp) hp = maxHp;
        if (hp < 0) hp = 0;
    }

    public virtual void Recalculate()
    {
        atk = Data.atk;
        finalAtk = atk + bonusAtk;
        finalSpd = (spd + bonusSpd) - moveCounter;

    }

    public void ResetStats()
    {

    }

    public void ApplyEffect(StatusEffect effect, BoardObject source)
    {
        if (Immunities.Contains(effect.effectTag)) return;
        statusEffects.Add(effect);
        effect.owner = this;
        effect.source = source;
        effect.ApplyEffect(this);
    }

    public virtual void Attack(BoardMob target)
    {
        if (validTarget.Contains(target))
        {
            float dmg = finalAtk;
            dmg = Utils.CalculateMobDamage(dmg, this, target);
            target.ChangeHealth(-dmg);
        }
    }

    public virtual void FinishAttack()
    {
        owner.EndAction();
        owner.selectedTile = null;
        owner.selectedObj = null;
        ResetTiles();
    }


    public virtual void Move()
    {
        if (owner.star >= moveCost || finalSpd >= moveCost)
        {

            if (validTiles.Count > 0 && validTiles.Contains(owner.selectedTile))
            {
                if (finalSpd >= moveCost)
                {
                    finalSpd -= moveCost;
                    moveCounter++;
                }
                else owner.star -= moveCost;

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
    }


    //? > Ketika state berubah tile atau unit yang kehighlight akan direset dan mengikuti kondisi
    public void ResetTiles()
    {
        foreach (var tile in validTiles)
        {
            tile.isHighlighted = false;
            if (owner.prevState == ActionState.Attack) if (tile.isOccupied && tile.activeObj.isHightlight) tile.activeObj.ToggleHighlight();
        }
        if (validTarget.Count > 0) validTarget.Clear();
        validTiles.Clear();

    }

    public void SetItem(Item target)
    {

    }


    public override void SelectThis()
    {
        if (statusEffects.Count > 0) foreach (var status in statusEffects) Debug.Log("status: " + status);
        ResetTiles();

        if (turn.activePlayer == owner)
        {
            if (statusEffects.Count > 0) foreach (var status in statusEffects) Debug.Log("status: " + status);
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
                owner.SelectMobSkill(this);
            }
            else if (owner.actState == ActionState.Place && owner.selectedObj.GetComponent<BoardObject>().type == UnitType.Item) owner.selectedObj.GetComponent<Item>().SetItem(this);
        }
        else
        {
            //? kalau unitnya beda owner dengna turn sekarang berarti pengen nyerang dan bukan select
            if (turn.activePlayer.actState == ActionState.Attack && turn.activePlayer.selectedObj != null)
            {
                turn.activePlayer.selectedSkill.ApplyEffect(this);
                turn.activePlayer.selectedObj.GetComponent<BoardMob>().FinishAttack();


            }
        }
    }

    public virtual void HighlightTarget()
    {
        foreach (var tile in validTiles) if (tile.isOccupied)
            {
                validTarget.Add(tile.activeObj);
                tile.activeObj.ToggleHighlight();
            }

    }

    public override void UnselectThis()
    {
        if (owner.selectedSkill != null) owner.selectedSkill.OnUnselected();
        owner.UnselectMob();
        ResetTiles();
        owner.selectedObj = null;
    }


    public override void OnActionDone()
    {
        if (statusEffects.Count > 0) for (int i = statusEffects.Count - 1; i >= 0; i--) statusEffects[i].OnActionDone();
        if (skills.Count > 0) foreach (var skill in skills) skill.OnActionDone();
        base.OnActionDone();
    }

    public override void OnTurnStart()
    {
        if (statusEffects.Count > 0) for (int i = statusEffects.Count - 1; i >= 0; i--) statusEffects[i].OnTurnStart();
        if (skills.Count > 0) foreach (var skill in skills) skill.OnTurnStart();
        base.OnTurnStart();
    }

    public override void OnTurnEnd()
    {
        if (statusEffects.Count > 0) for (int i = statusEffects.Count - 1; i >= 0; i--) statusEffects[i].OnTurnEnd();
        if (skills.Count > 0) foreach (var skill in skills) skill.OnTurnEnd();
        moveCounter = 0;
        finalSpd = (spd + bonusSpd);
        base.OnTurnEnd();
    }

    public override void OnTurnFinish()
    {
        if (statusEffects.Count > 0) for (int i = statusEffects.Count - 1; i >= 0; i--) statusEffects[i].OnTurnFinish();
        if (skills.Count > 0) foreach (var skill in skills) skill.OnTurnFinish();
        base.OnTurnFinish();
    }



}