using UnityEngine;


[System.Serializable]
public class Card
{

    public ObjectData data;
    public bool isDrawable;
    public int count;
    //?Container is where the card is being held. e.g Deck, Hand
    public Container container;
    public GameObject unitPrefab;

    
}
