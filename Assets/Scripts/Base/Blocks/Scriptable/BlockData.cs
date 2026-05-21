using UnityEngine;

[CreateAssetMenu(fileName = "BlockData", menuName = "Block")]
public class BlockData : ObjectData
{
    public string blockName;
    public int effectRange;

    public Vector2Int[] effectDir = new Vector2Int[]{
        new (0, 1),   // atas
        new (0, -1),  // bawah
        new (1, 0),   // kanan
        new (-1, 0),  // kiri

        new (1, 1),   // kanan atas
        new (1, -1),  // kanan bawah
        new (-1, 1),  // kiri atas
        new (-1, -1), // kiri bawah
    };


    private void OnValidate()
    {
        blockName = name;
    }



}

