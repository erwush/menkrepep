using UnityEngine;
using System.Collections.Generic;

public class ObjectManager : MonoBehaviour
{
    public Player player;
    public int activeMob;
    public List<GameObject> activeUnits = new List<GameObject>();
    public List<Card> boardhand, deck, discard, startingDeck = new List<Card>();


    public void SelectObject(GameObject obj)
    {
        if (player.selectedObj != null && player.inventory.activeUnits.Contains(player.selectedObj)) 
            player.selectedObj.GetComponent<BoardObject>().UnselectThis();
        player.selectedObj = obj;
        player.ChangeState("Place");

    }

    public void InitializeDeck()
    {
        foreach(var obj in startingDeck) deck.Add(obj);
    }

    public void ShuffleDeck()
    {
        for(int i = 0; i < deck.Count; i++)
        {
            var temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    public void DrawCard(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            boardhand.Add(deck[0]);
            deck.RemoveAt(0);
            
        }
    }

    public void DiscardCard(Card card)
    {
        switch (card.container)
        {
            case Container.Deck:
                discard.Add(card);
                deck.Remove(card);
                break;
            case Container.Hand:
                discard.Add(card);
                boardhand.Remove(card);
                break;
        }
    }

    public void ReshuffleDiscard()
    {
        foreach(var obj in discard)
        {
            if (!obj.isDrawable) continue;

            deck.Add(obj);
            discard.Remove(obj);
        }
    }


}