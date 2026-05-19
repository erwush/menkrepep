using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Inventory inv;
    public GameObject selectedObj, targetObj;
    public MobSkill selectedSkill;
    public string playerName;
    public Tile selectedTile;

    public List<GameObject> activeUnits;
    public ActionState actState;
    public ActionState prevState;
    public Button[] actBtn;
    public int star, maxStar, ultStar, maxUltStar;
    public GameObject displayObj, displayParent, displayPanel;
    public GameObject skillDisplay, skillParent, skillPanel;
    public Dictionary<BoardMob, UnitDisplay> displays;
    public TextMeshProUGUI starText, nameText;
    public UiManager menu;
    public bool isTargeting;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actState = ActionState.Idle;
        prevState = ActionState.Idle;
        displays = new Dictionary<BoardMob, UnitDisplay>();
        RefreshDisplay();
    }
    
    public void Awake()
    {
        menu = UiManager.Instance;
    }

    public void ConfirmAttack(){
        if (actState == ActionState.Attack)
        {
            if (isTargeting)
            {
                selectedSkill.ApplyEffect(selectedObj.GetComponent<BoardMob>());
            }
        }
        selectedObj.GetComponent<BoardMob>().FinishAttack();
    }


    public void ChangeState(string stateName)
    {
        if (System.Enum.TryParse(stateName, true, out ActionState newState))
        {
            prevState = actState;
            actState = newState;
            if (activeUnits.Contains(selectedObj) && (prevState == ActionState.Move || prevState == ActionState.Attack)) selectedObj.GetComponent<BoardObject>().SelectThis();
            for (int i = 0; i < actBtn.Length; i++)
            {
                if (actBtn[i].name.Equals(actState.ToString()))
                {
                    Button currentBtn = actBtn[i];
                    actBtn[i].GetComponent<Image>().sprite = actBtn[i].spriteState.pressedSprite;
                    foreach (var btn in actBtn)
                    {
                        if (btn != currentBtn)
                        {
                            btn.GetComponent<Image>().sprite = btn.spriteState.disabledSprite;
                        }
                    }
                }
            }
        }
    }


    public void EndAction()
    {

        ChangeState("Idle");
        foreach (var unit in activeUnits)
        {
            unit.GetComponent<BoardObject>().UnselectThis();

            if (unit.GetComponent<BoardObject>().type == UnitType.Block) unit.GetComponent<BoardObject>().OnActionDone();
            else if (unit.GetComponent<BoardObject>().type == UnitType.Mob) unit.GetComponent<BoardMob>().Recalculate();
        }

        foreach (var player in TurnManager.Instance.players) player.RefreshDisplay();

    }


    public void RefreshDisplay()
    {
        if (displays.Count > 0)
        {
            foreach (var disp in displays)
            {
                disp.Value.UpdateUI();
            }
        }
        starText.text = star.ToString();
        nameText.text = playerName;
    }

    public void RegisterUnit(GameObject obj)
    {
        activeUnits.Add(obj);
        
        // Debug.Log(obj.name);
        BoardObject unit = obj.GetComponent<BoardObject>();
        if (unit is BoardMob mob)
        {
            ChangeStar(-mob.data.cost);

            mob.Recalculate();
            UnitDisplay disp = Instantiate(displayObj, displayParent.transform).GetComponent<UnitDisplay>();
            disp.Setup(mob);
            displays.Add(mob, disp);
            disp.UpdateUI();

        }
        else if (unit is BoardBlock block)
        {
            ChangeStar(-block.data.cost);
        }
        unit.owner = this;
    }

    public void UnregisterUnit(BoardObject obj)
    {

    }

    public void SelectMobSkill(BoardMob mob)
    {
        foreach (var disp in menu.selectedDisplay)
        {
            Destroy(disp.gameObject);
        }
        menu.selectedDisplay.Clear();
        SkillDisplay display = Instantiate(skillDisplay, skillParent.transform).GetComponent<SkillDisplay>();
        display.Setup(null);
        menu.selectedDisplay.Add(display);
        foreach (var skill in mob.skills)
        {
            SkillDisplay disp = Instantiate(skillDisplay, skillParent.transform).GetComponent<SkillDisplay>();
            disp.Setup(skill);
            menu.selectedDisplay.Add(disp);
        }
        menu.selectedDisplay[0].SelectSkill();
    }

    

    public void UnselectMob()
    {
        foreach (var disp in menu.selectedDisplay) Destroy(disp.gameObject); 
        menu.selectedDisplay.Clear();
        
    }
    
    

    public void ChangeStar(int amount)
    {
        star += amount;
        if (star > maxStar) star = maxStar;
        if (star < 0) star = 0;
    }
    
    public void ChangeUltStar(int amount)
    {
        ultStar += amount;
        if (ultStar > maxUltStar) ultStar = maxUltStar;
        if (ultStar < 0) ultStar = 0;
    }

    
}

public enum ActionState
{
    Idle,
    Move,
    Attack,
    Place
}
