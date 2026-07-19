using UnityEngine;
using System.Collections.Generic;

public class ObjectManager : MonoBehaviour
{
    public Player player;
    public int activeMob;
    public List<GameObject> activeUnits = new List<GameObject>();
    public List<BoardObject> boardhand, deck, discard, startingDeck = new List<BoardObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
    }

    public void SelectObject(GameObject obj)
    {
        if (player.selectedObj != null && player.inventory.activeUnits.Contains(player.selectedObj)) player.selectedObj.GetComponent<BoardObject>().UnselectThis();
        player.selectedObj = obj;
        player.ChangeState("Place");

    }

    public void InitializeDeck()
    {

    }

    public void ShuffleDeck()
    {

    }

    public void DrawCard()
    {

    }

    public void DiscardCard()
    {

    }


}