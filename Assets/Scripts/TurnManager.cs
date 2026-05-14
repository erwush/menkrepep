using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public Player[] players;
    public Player activePlayer;
    public int currentIndex;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void NextTurn()
    {
        currentIndex++;
        if (currentIndex >= players.Length) currentIndex = 0;
        activePlayer = players[currentIndex];
    }
}
