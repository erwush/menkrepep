using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SkillDisplay : MonoBehaviour
{
    public BoardMob data;
    public MobSkill skill;
    public TextMeshProUGUI nameText, descText;
    public GameObject displayUi;
    public Sprite[] sprite; //0: normal, 1: selected
    public Image img;
    public Player player;
    public bool isSelected;


    public void UpdateUI()
    {
        if (skill != null)
        {
            nameText.text = skill.data.skillName;
            descText.text = skill.data.skillDesc;
        }
    }

    public void Setup(MobSkill mobSkill)
    {
        if (mobSkill != null)
        {
            data = mobSkill.owner;
            player = data.owner;
            skill = mobSkill;
        }
        else
        {
            player = TurnManager.Instance.activePlayer;
            data = player.selectedObj.GetComponent<BoardMob>();
            skill = null;
        }
        UpdateUI();
    }

    public void SelectSkill()
    {
        foreach (var disp in player.menu.selectedDisplay) if (disp != this) disp.Unselect();
        player.selectedSkill = skill;
        if (player.selectedSkill != null) player.selectedSkill.OnSelected();
        img.sprite = sprite[1];
    }

    public void Unselect()
    {
        img.sprite = sprite[0];
        if(skill != null) skill.OnUnselected();
    }




}
