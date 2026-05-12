using UnityEngine;

public abstract class BoardObject : MonoBehaviour
{

    public Tile tile;
    public Tile currentTile;
    public Player player;
    public BoardSystem board;

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
        player.selectedObj = gameObject;
    }

    public virtual void UnselectThis()
    {
        player.selectedObj = null;
    }

    public abstract void Move();

}
