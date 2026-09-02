using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EditorAttributes;

public class GameManager : MonoBehaviour
{













    #region DiceRoll


    public Sprite[] slotSprite; //0 = unselected, 1 = selected
    public Image[] numberSlots;

    
    [Button("Roll")]
    public int DiceRoll()
    {
        foreach(var slot in numberSlots) slot.sprite = slotSprite[0];
        int roll = Random.Range(1, 10);
        StartCoroutine(RollAnim(roll));
        return roll;
    }

    public IEnumerator RollAnim(int roll)
    {
        for (int i = 0; i < 11; i++)
        {
            int rand = Random.Range(0, 9);
            numberSlots[rand].sprite = slotSprite[1];
            yield return new WaitForSeconds(0.1f);
            numberSlots[rand].sprite = slotSprite[0];
        }
        numberSlots[roll-1].sprite = slotSprite[1];
    }

    #endregion
}
