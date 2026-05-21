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



    void Awake()
    {
        Instance = this;
    }

    void CreateBoard()
    {
        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x, 0, y);

                //? ngecek posisi genap atau ganjil
                int color = (x + y) % 2;

                tiles[x, y] = Instantiate(
                    tilePrefab[color],
                    pos,
                    Quaternion.identity,
                    tileParent.transform
                ).GetComponent<Tile>();

                tiles[x, y].Setup(x, y);
            }
        }

        tileParent.transform.position =
            new Vector3((-width / 2f)+0.5f, 0, (-height / 2f)+0.5f);
    }

    public Tile GetTile(int x, int y)
    {
        // Cek batas board
        if (x < 0 || y < 0 || x >= width || y >= height)
            return null;

        return tiles[x, y];
    }
}
