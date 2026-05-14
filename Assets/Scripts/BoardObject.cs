using UnityEngine;

public abstract class BoardObject : MonoBehaviour
{

    public Tile tile;
    public Tile currentTile;
    public BoardManager board;
    public bool isHightlight;
    public Player owner;
    public Material[] Mat; //0 = default, 1 = hightlighted

    public int x
    {
        get
        {
            return tile.x;
        }
    }
    public int y
    {
        get
        {
            return tile.y;
        }
    }

    public virtual void SelectThis()
    {
        owner.selectedObj = gameObject;
    }

    public virtual void UnselectThis()
    {
        owner.selectedObj = null;
    }

    public virtual void Move()
    {

    }
    

    

    public virtual void ToggleHightlight()
    {
        if (isHightlight)
        {
            Material self = GetComponent<MeshRenderer>().material = Mat[0];
            isHightlight = false;
        } else
        {
            Material self = GetComponent<MeshRenderer>().material = Mat[1];
            isHightlight = true;
        }
    }

}
