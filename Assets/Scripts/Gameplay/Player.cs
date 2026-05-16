using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Inventory inv;
    public GameObject selectedObj;
    public Tile selectedTile;

    public List<GameObject> activeUnits;
    public ActionState actState;
    public ActionState prevState;
    public Button[] actBtn;
    public int star;
    public UnitDisplay display;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actState = ActionState.Idle;
        prevState = ActionState.Idle;
    }

    // Update is called once per frame
    void Update()
    {

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
        // display.UpdateUI();
        
    }
}

public enum ActionState
{
    Idle,
    Move,
    Attack,
    Place
}
