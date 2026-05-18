using UnityEngine;

public abstract class Item : BoardObject
{
    public int amount;
    public BoardMob holder;
    public ItemData Data => data as ItemData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void SetItem(BoardMob target)
    {
        
    }

    public virtual void OnHeld()
    {
       
    }
    
    public virtual void OnThrown()
    {

    }

}
