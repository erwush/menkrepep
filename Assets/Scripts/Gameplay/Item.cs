using UnityEngine;

public abstract class Item : MonoBehaviour
{

    public BoardMob holder;
    public ItemData data;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnTurnEnd()
    {

    }

    public virtual void OnTurnStart()
    {

    }

    public virtual void OnActionDone()
    {

    }
}
