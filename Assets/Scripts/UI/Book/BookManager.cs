using UnityEngine;

public class BookManager : MonoBehaviour
{
    public static BookManager Instance;

    void Awake()
    {
        Instance = this;
    }
}
