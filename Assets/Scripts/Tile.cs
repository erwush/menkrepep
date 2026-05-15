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
    public BoardObject activeObj;
    private bool isHover;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = TurnManager.Instance.activePlayer;
        outline.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isHighlighted) outline.SetActive(true);
        else outline.SetActive(false);

        if (player.actState == ActionState.Move && !isHover) outline.GetComponent<Renderer>().material = outMat[1];
        else if (player.actState == ActionState.Attack && !isHover) outline.GetComponent<Renderer>().material = outMat[2];
        else outline.GetComponent<Renderer>().material = outMat[0];

    }

    public void Setup(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void TileSelect()
    {
        if (player.actState == ActionState.Move)
        {

            if (!isOccupied && player.selectedObj != null && player.actState == ActionState.Move)
            {
                if (player.selectedObj.GetComponent<BoardObject>().type == UnitType.Mob)
                {
                    isOccupied = true;
                    activeObj = player.selectedObj.GetComponent<BoardObject>();
                    player.selectedTile = this;
                    player.selectedObj.GetComponent<BoardMob>().Move();
                }
            }
        }
        else if (player.actState == ActionState.Place)
        {
            if (!isOccupied && player.selectedObj != null && player.actState == ActionState.Place)
            {
                GameObject obj = Instantiate(player.selectedObj, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
                BoardObject unit = obj.GetComponent<BoardObject>();
                unit.owner = player;
                unit.currentTile = this;
                isOccupied = true;
                activeObj = obj.GetComponent<BoardObject>();
                player.activeUnits.Add(obj);
                player.selectedObj = null;
                player.EndAction();
            }
        }
    }

    public void HoverEnter()
    {
        isHover = true;
        if (!player.actState.Equals(ActionState.Move) && player.actState != ActionState.Attack && !isOccupied) isHighlighted = true;
    }

    public void HoverExit()
    {
        isHover = false;
        if (!player.actState.Equals(ActionState.Move) && player.actState != ActionState.Attack) isHighlighted = false;
    }
}
