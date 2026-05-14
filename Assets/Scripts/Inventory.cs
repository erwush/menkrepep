using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Player player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void SelectObject(GameObject obj)
    {
        if(player.selectedObj != null && player.activeUnits.Contains(player.selectedObj)) player.selectedObj.GetComponent<BoardObject>().UnselectThis();
        player.selectedObj = obj;
        player.ChangeState("Place");
        
    }
}
