using UnityEngine;

[CreateAssetMenu(fileName = "MobData", menuName = "Mob")]
public class MobData : ScriptableObject
{
    public string mobName;
    public float maxHp, atk;
    public int atkRange, moveRange, cost = 1;
    public Category category;
    public Sprite sprite;
    public Vector2Int[] moveDir = new Vector2Int[]{
        new (0, 1),   // atas
        new (0, -1),  // bawah
        new (1, 0),   // kanan
        new (-1, 0),  // kiri

        new (1, 1),   // kanan atas
        new (1, -1),  // kanan bawah
        new (-1, 1),  // kiri atas
        new (-1, -1), // kiri bawah
    };
    public Vector2Int[] atkDir = new Vector2Int[]{
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
        mobName = name;
    }



}

public enum Category
{
    Undead,
    Illager,
    Nether,
    Aquatic,
    Animal,
    Alien
}
