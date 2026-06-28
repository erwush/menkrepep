using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SkillDisplay : MonoBehaviour
{
    public BoardMob data;
    public MobSkill skill;
    //?cur stands for current
    public TextMeshProUGUI nameText, costText, cdText, curCdText, nameDetail, descDetail, costDetail;
    public GameObject displayUi, detailUi;
    public Player player;
    public bool isSelected;
    public UiManager menu;
    public ExplainSystem explain;
    public Button selectBtn;
    public Color[] bgColor; //0: passive, 1: ultimate
    public Image bgImg, typeImg, costImg;
    public Sprite[] sprite; //0: normal, 1: selected
    public Sprite[] typeSprite; //0: passive, 1: self, 2: single, 3: single ranged, 4: area
    public Sprite[] costSprite; //0: shard, 1: star (for ultimate)

    public void Start()
    {
        menu = UiManager.Instance;
        explain = ExplainSystem.Instance;
        nameDetail = menu.nameDetail;
        descDetail = menu.descDetail;
        costDetail = menu.costDetail;
        detailUi = menu.detailUi;
    }


    public void UpdateUI()
    {
        if (skill != null)
        {
            nameText.text = skill.data.skillName;
            cdText.text = skill.data.cooldown.ToString();
            curCdText.text = skill.curCooldown.ToString();
            bgImg.sprite = sprite[0];
            if (skill.data.isUltimate)
            {
                costText.text = (skill.data.ultCost).ToString();

                costImg.sprite = costSprite[1];
                bgImg.color = bgColor[1];
            }
            else
            {
                costText.text = (skill.data.cost + skill.owner.costReduction).ToString();

                costImg.sprite = costSprite[0];
            }
            if (skill.data.skillType == SkillType.Passive)
            {
                typeImg.sprite = typeSprite[0];
                bgImg.color = bgColor[0];
                selectBtn.interactable = false;
            }
            else
            {
                if (skill.data.rangeType == RangeType.Self) typeImg.sprite = typeSprite[1];
                else if (skill.data.rangeType == RangeType.Single && (skill.data.atkRange + skill.owner.atkRange) == 0) typeImg.sprite = typeSprite[2];
                else if (skill.data.rangeType == RangeType.Single && (skill.data.atkRange + skill.owner.atkRange) > 0) typeImg.sprite = typeSprite[3];
                else if (skill.data.rangeType == RangeType.Area) typeImg.sprite = typeSprite[4];

            }

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
        foreach (var disp in player.menu.selectedDisplay) if (disp != this) disp.UnselectSkill();
        player.selectedSkill = skill;
        if (player.selectedSkill != null) player.selectedSkill.OnSelected();
        bgImg.sprite = sprite[1];
    }

    public void UnselectSkill()
    {
        bgImg.sprite = sprite[0];
        if (skill != null) skill.OnUnselected();
    }

    public void OpenDetail()
    {
        if (!menu.detailOpen) detailUi.SetActive(true);
        nameDetail.text = skill.skillName;
        descDetail.text = skill.skillDesc;
        costDetail.text = skill.costDesc;
        menu.detailOpen = true;
        explain.SetupExplain(skill.data);
        // costDetail.text;
    }








}
