using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x;
    public int y;
    public bool isOccupied;
    public Player player;
    public GameObject outline;
    public bool isHighlighted;
    public Material[] outMat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        outline.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isHighlighted) outline.SetActive(true);
        else outline.SetActive(false);

        if (player.isMoving) outline.GetComponent<Renderer>().material = outMat[1];
        else outline.GetComponent<Renderer>().material = outMat[0];
        
    }

    public void Setup(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void TileSelect()
    {
        if (!isOccupied && player.selectedObj != null && !player.isMoving)
        {
            GameObject obj = Instantiate(player.selectedObj, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
            Zombie zombie = obj.GetComponent<Zombie>();
            zombie.currentTile = this;
            isOccupied = true;
            player.selectedObj = null;
        } else if (!isOccupied && player.selectedObj != null && player.isMoving)
        {
            player.selectedTile = this;
            player.selectedObj.GetComponent<BoardObject>().Move();
        }
    }

    public void HoverEnter()
    {
        if(!player.isMoving) isHighlighted = true;
    }

    public void HoverExit()
    {
        if(!player.isMoving) isHighlighted = false;
    }
}
