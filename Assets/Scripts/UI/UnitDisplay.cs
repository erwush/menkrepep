using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UnitDisplay : MonoBehaviour
{
    public BoardMob data;
    public Slider healthBar;
    public TextMeshProUGUI healthText, atkText, bonusAtkText;
    public Image iconImg;
    public GameObject displayUi;
    public Player player;
    
  
    public void UpdateUI()
    {
        iconImg.sprite = data.Data.sprite;
        healthBar.value = data.hp / data.maxHp;
        healthText.text = data.hp.ToString() + "/" + data.maxHp.ToString();
        atkText.text = data.atk.ToString();
        bonusAtkText.text = data.bonusAtk.ToString();
    }

    public void Setup(BoardObject obj)
    {
        data = obj.GetComponent<BoardMob>();
        player = obj.GetComponent<BoardObject>().owner;
        UpdateUI();
    }

   

    
}
