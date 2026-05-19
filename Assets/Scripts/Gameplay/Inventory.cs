using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Player player;
    public BoardObject slot;


    
    public void SelectObject(GameObject obj)
    {
        if(player.selectedObj != null && player.activeUnits.Contains(player.selectedObj)) player.selectedObj.GetComponent<BoardObject>().UnselectThis();
        player.selectedObj = obj;
        player.ChangeState("Place");
        
    }
}
