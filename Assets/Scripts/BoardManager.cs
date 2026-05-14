using UnityEngine;

public class BoardManager : MonoBehaviour
{

    public static BoardManager Instance;    
    public int width;
    public int height;

    public GameObject[] tilePrefab;
    public GameObject tileParent;

    public Tile[,] tiles;

    void Start()
    {
        CreateBoard();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        Instance = this;
    }

    void CreateBoard()
    {
        int color = 0;
        tiles = new Tile[width, height];
        for (int x = 0; x < width; x++)
        {

            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x, 0, y);
                if (color == 0)
                {
                    color = 1;
                    tiles[x, y] = Instantiate(tilePrefab[0], pos, Quaternion.identity, tileParent.transform).GetComponent<Tile>();
                    tiles[x, y].Setup(x, y);
                }
                else
                {
                    color = 0;
                    tiles[x, y] = Instantiate(tilePrefab[1], pos, Quaternion.identity, tileParent.transform).GetComponent<Tile>();
                    tiles[x, y].Setup(x, y);
                }
            }
        }
        tileParent.transform.position = new Vector3(-width / 2, 0, -height / 2);
    }

    public Tile GetTile(int x, int y)
    {
        // Cek batas board
        if (x < 0 || y < 0 || x >= width || y >= height)
            return null;

        return tiles[x, y];
    }
}
